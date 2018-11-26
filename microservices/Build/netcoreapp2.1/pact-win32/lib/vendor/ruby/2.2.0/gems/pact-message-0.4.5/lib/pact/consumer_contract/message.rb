require 'pact/consumer_contract/message/contents'
require 'pact/symbolize_keys'
require 'pact/shared/active_support_support'
require 'pact/matching_rules'
require 'pact/errors'
require 'pact/consumer/request'
require 'pact/consumer_contract/response'
require 'pact/consumer_contract/message/contents'

module Pact
  class ConsumerContract
    class Message
      include Pact::ActiveSupportSupport
      include Pact::SymbolizeKeys

        attr_accessor :description, :contents, :provider_state, :metadata

        def initialize attributes = {}
          @description = attributes[:description]
          @provider_state = attributes[:provider_state] || attributes[:providerState]
          @contents = attributes[:contents]
          @metadata = attributes[:metadata]
        end

        def self.from_hash hash, options = {}
          opts = options.dup
          unless opts[:pact_specification_version]
            opts[:pact_specification_version] = Pact::SpecificationVersion::NIL_VERSION
          end
          contents_matching_rules = hash['matchingRules'] && hash['matchingRules']['body']
          contents_hash = Pact::MatchingRules.merge(hash['contents'], contents_matching_rules, opts)
          contents = Pact::ConsumerContract::Message::Contents.from_hash(contents_hash)
          metadata = hash['metaData'] || hash['metadata']
          provider_state = hash['providerStates'] && hash['providerStates'].first && hash['providerStates'].first['name']
          warn_if_multiple_provider_states(provider_state, hash)
          warn_if_params_used_in_provider_states(hash)
          new(symbolize_keys(hash).merge(contents: contents, provider_state: provider_state, metadata: metadata))
        end

        def to_hash
          {
            description: description,
            provider_states: [{ name: provider_state }],
            contents: contents.to_hash,
            metadata: metadata
          }
        end

        def request
          @request ||= Pact::Consumer::Request::Actual.from_hash(
            path: '/',
            method: 'POST',
            query: nil,
            headers: { 'Content-Type' => 'application/json' },
            body: {
              description: description,
              providerStates: [{
                name: provider_state,
                params: {}
              }]
            }
          )
        end

        # custom media type?
        def response
          @response ||= Pact::Response.new(
            status: 200,
            headers: {'Content-Type' => 'application/json'},
            body: {
              contents: contents
            }
          )
        end

        def http?
          false
        end

        def message?
          true
        end

        def validate!
          raise Pact::InvalidMessageError.new(self) unless description && contents
        end

        def == other
          other.is_a?(Message) && to_hash == other.to_hash
        end

        def matches_criteria? criteria
          criteria.each do | key, value |
            unless match_criterion self.send(key.to_s), value
              return false
            end
          end
          true
        end

        def match_criterion target, criterion
          target == criterion || (criterion.is_a?(Regexp) && criterion.match(target))
        end

        def eq? other
          self == other
        end

        def description_with_provider_state_quoted
          provider_state ? "\"#{description}\" given \"#{provider_state}\"" : "\"#{description}\""
        end

        def to_s
          to_hash.to_s
        end

        private

        def self.warn_if_multiple_provider_states(provider_state, hash)
          if hash['providerStates'] && hash['providerStates'].size > 1
            ignored_list = hash['providerStates'].collect{ |provider_state| "\"#{provider_state['name']}\"" }[1..-1].join(", ")
            Pact.configuration.error_stream.puts("WARN: Using only the first provider state, \"#{provider_state}\", as support for multiple provider states is not yet implemented. Ignoring provider states: #{ignored_list}")
          end
        end

        def self.warn_if_params_used_in_provider_states(hash)
          return unless hash['providerStates']
          provider_states_with_params = hash['providerStates'].select{ | provider_state | provider_state.fetch('params', {}).any? }
          if provider_states_with_params.any?
            ignored_list = provider_states_with_params.collect{ |provider_state| "\"#{provider_state['name']}\"" }.join(", ")
            Pact.configuration.error_stream.puts("WARN: Ignoring params for the following provider states as params support is not yet implemented: #{ignored_list}")
          end
        end
    end
  end
end

module Pact
  module Message
    def self.new *args
      Pact::ConsumerContract::Message.new(*args)
    end

    def self.from_hash *args
      Pact::ConsumerContract::Message.from_hash(*args)
    end
  end
end