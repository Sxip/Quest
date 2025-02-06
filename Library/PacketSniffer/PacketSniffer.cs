using System;
using Library.Harmony;
using Newtonsoft.Json;
using UnityEngine;

namespace Library.PacketSniffer
{
    public static class PacketSniffer
    {
        /// <summary>
        /// Provides custom serialization settings for handling JSON data.
        /// Configured to ignore default and null values during serialization
        /// and deserialization processes.
        /// </summary>
        public static readonly JsonSerializerSettings SerializerSettings = new JsonSerializerSettings
        {
            DefaultValueHandling = DefaultValueHandling.Ignore,
            NullValueHandling = NullValueHandling.Ignore
        };

        /// <summary>
        /// Indicates the current operational state of the packet sniffer,
        /// specifying whether it is actively capturing network packets.
        /// </summary>
        public static bool IsRunning { get; set; }

        /// <summary>
        /// Provides access to the singleton instance of the AEC class.
        /// Facilitates interaction with AEC-specific functionalities
        /// within the PacketSniffer module.
        /// </summary>
        private static AEC AecInstance => AEC.getInstance();

        public static event Action<string> PacketReceived;
        public static event Action<string> PacketSent;

        /// <summary>
        /// Sends a network request for processing. This method is designed to
        /// trigger the PacketSent event, which allows external components to
        /// respond to and handle the provided network request.
        /// </summary>
        /// <param name="request">The network request data to be sent and processed.</param>
        public static void SendRequest(string request) => PacketSent?.Invoke(request);

        /// <summary>
        /// Initiates the packet-sniffing process. The method sets up necessary
        /// configurations and begins capturing network packets, enabling analysis
        /// of network traffic. This method is intended to be called to start
        /// the packet-sniffing functionality within the application.
        /// </summary>
        public static void Start()
        {
            try
            {
                AecInstance.ResponseReceived += ResponseReceived;
                IsRunning = true;
            }
            catch
            {
                Debug.LogError("Error starting PacketSniffer.");
            }
        }

        /// <summary>
        /// Stops the packet-sniffing process, disabling active components and
        /// unsubscribing from event handlers. The method ensures that the
        /// packet-sniffing operations cease and associated resources are released.
        /// </summary>
        /// <remarks>
        /// When invoked, this method checks if the sniffing process is currently
        /// running. If it is, active hooks and response handlers are removed,
        /// events are unsubscribed, and the running state is set to false.
        /// A diagnostic log entry is generated to indicate that the packet-sniffing
        /// process has been successfully stopped.
        /// </remarks>
        public static void Stop()
        {
            if (!IsRunning) return;

            AecInstance.ResponseReceived -= ResponseReceived;

            IsRunning = false;
            Debug.LogWarning("PacketSniffer stopped.");
        }


        /// <summary>
        /// Handles the event when a response is received, processing and intercepting
        /// the provided response data for further use or analysis.
        /// </summary>  
        /// <param name="response">The response object received, representing data
        /// transmitted during network communication.</param>
        /// <remarks>
        /// This method serializes the response object into a JSON format as a string,
        /// logs the intercepted response for debugging purposes, and invokes the
        /// subscribed PacketReceived event with the serialized data. Any errors occurring
        /// during the process are logged for troubleshooting.
        /// </remarks>
        /// <exception cref="System.Exception">
        /// Thrown if an error occurs during the serialization or event invocation process,
        /// including failures to serialize the response object or raise the PacketReceived event.
        /// </exception>
        private static void ResponseReceived(Response response)
        {
            try
            {
                var serializedResponse = JsonConvert.SerializeObject(response, Formatting.None, SerializerSettings);
                Debug.LogWarning($"Intercepted ResponseReceived: {serializedResponse}");

                PacketReceived?.Invoke(serializedResponse);
            }
            catch (Exception exception)
            {
                Debug.LogError($"Error processing ResponseReceived: {exception.Message}");
            }
        }
    }
}