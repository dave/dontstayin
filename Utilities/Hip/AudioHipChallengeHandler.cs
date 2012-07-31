// Stephen Toub
// stoub@microsoft.com

using System;
using System.IO;
using System.Web;
using SpeechLib;

namespace Msdn.Web.UI.WebControls
{
	/// <summary>Handles requests for dynamic WAV files from the AudioHipChallenge control.</summary>
	public class AudioHipChallengeHandler : BaseHipChallengeHandler, IHttpHandler
	{
		/// <summary>Gets whether this handler is reusable.</summary>
		/// <remarks>This handler is not thread-safe (uses non thread-safe member variables), so it is not reusable.</remarks>
		public bool IsReusable { get { return false; } }

		/// <summary>Processes the request for the sound file.</summary>
		/// <param name="context">The current HttpContext.</param>
		public void ProcessRequest(HttpContext context)
		{
			// Get the ID information
			string text = HipChallenge.GetChallengeText(new Guid(context.Request.QueryString[AudioHipChallenge.ID_KEY]));

			// If we got the text for the challenge (meaning that the ID is valid and the text hasn't expired),
			// generate the audio and send it along to the client.
			if (text != null)
			{
				// Get a path for the temporary audio file.  It's constructed as follows due to a sort
				// of catch-22.  Path.GetTempFileName can't be used because the file needs to end in
				// ".wav" for HttpResponse.WriteFile to work correctly.  We can't use it and then rename it
				// to end in ".wav" as we then aren't guaranteed uniqueness.  So, we're left to construct
				// a temp name manually, and the easiest way to do that is with GUIDs.
				FileInfo tempAudio = new FileInfo(Path.GetTempPath() + "/" + "aud" + Guid.NewGuid().ToString("N") + ".wav");
				try
				{
					// Speak the data to the file
					SpeakToFile(text, tempAudio);

					// Send the audio to the client
					HttpResponse resp = context.Response;
					resp.ContentType = "audio/wav";
					resp.WriteFile(tempAudio.FullName, true);
				}
				finally
				{
					// Delete the temporary audio file
					tempAudio.Delete();
				}
			}
		}

		/// <summary>Speaks the specified text to the specified file.</summary>
		/// <param name="text">The text to be spoken.</param>
		/// <param name="audioPath">The file to which the spoken text should be written.</param>
		/// <remarks>Uses the Microsoft Speech libraries.</remarks>
		private void SpeakToFile(string text, FileInfo audioPath)
		{
			SpFileStream spFileStream = new SpFileStream();
			try
			{
				// Create the speech engine and set it to a random installed voice
				SpVoice speech = new SpVoice();
				ISpeechObjectTokens voices = speech.GetVoices(string.Empty, string.Empty);
				speech.Voice = voices.Item(NextRandom(voices.Count));

				// Set the format type to be heavily compressed.  This both decreases download
				// size and increases distortion.
				SpAudioFormatClass format = new SpAudioFormatClass();
				format.Type = SpeechAudioFormatType.SAFTGSM610_11kHzMono;
				spFileStream.Format = format;

				// Open the output stream for the file, speak to it, and wait until it's complete
				spFileStream.Open(audioPath.FullName, SpeechStreamFileMode.SSFMCreateForWrite, false);
				speech.AudioOutputStream = spFileStream;
				speech.Speak(text, SpeechVoiceSpeakFlags.SVSFlagsAsync);
				speech.Rate = -5;
				speech.WaitUntilDone(System.Threading.Timeout.Infinite);
			}
			finally
			{
				// Close the output file
				spFileStream.Close();
			}
		}
	}
}
