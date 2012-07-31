// Stephen Toub
// stoub@microsoft.com

using System;
using System.IO;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Security.Cryptography;
using System.Drawing;
using System.ComponentModel;
using SpeechLib;

namespace Msdn.Web.UI.WebControls
{
	/// <summary>An audio Reverse Turing Test (HIP).</summary>
	public class AudioHipChallenge : HipChallenge
	{
		/// <summary>Query string key for the audio-challenge data.</summary>
		internal const string ID_KEY = "d";

		/// <summary>Default value for the AutoStart property.</summary>
		private const bool AUTOSTART_DEFAULT = false;
		/// <summary>Default value for the ShowPlayButton property.</summary>
		private const bool SHOWPLAY_DEFAULT = true;
		/// <summary>Default value for the SpellWords property.</summary>
		private const bool SPELLWORDS_DEFAULT = true;
		/// <summary>Default value for the Text property.</summary>
		private const string TEXT_DEFAULT = "Listen To Challenge";
		/// <summary>Default value for the RenderUrl property.</summary>
		private const string RENDERURL_DEFAULT = "/AudioHipChallenge.aspx";
		
		/// <summary>Backing store for the AutoStart property.</summary>
		private bool _autoStart = AUTOSTART_DEFAULT;
		/// <summary>Backing store for the ShowPlayButton property.</summary>
		private bool _showPlayButton = SHOWPLAY_DEFAULT;
		/// <summary>Backing store for the SpellWords property.</summary>
		private bool _spellWords = SPELLWORDS_DEFAULT;
		/// <summary>Backing store for the Text property.</summary>
		private string _text = TEXT_DEFAULT;
		/// <summary>Backing store for the RenderUrl property.</summary>
		private string _renderUrl = RENDERURL_DEFAULT;

		/// <summary>Used to map letters to their spoken pronunciations.</summary>
		/// <remarks>
		/// Users have the option of having words spelled out rather than pronounced in full.  However, the TTS engine
		/// needs some help pronouncing letters as letters.  This mapping is used to go from a letter to a spelling that
		/// supplies the TTS engine with a better pronunciation.
		/// </remarks>
		private static string [] _spelledLetters = {"hay", "bee", "see", "dee", "ee", "ef", "gee", "haych", "eye", "jay", "kay", "el", "em", "en", "oh", "pee", "queue", "are", "es", "tee", "you", "vee", "double you", "ex", "why", "zee"};

		/// <summary>Gets or sets the URL used to render the sound to the client's media player.</summary>
		[Category("Behavior")]
		[DefaultValue(RENDERURL_DEFAULT)]
		public string RenderUrl 
		{ 
			get { return _renderUrl; } 
			set 
			{
				if (value == null || value.Trim().Length == 0) throw new ArgumentNullException("RenderUrl");
				_renderUrl = value; 
			} 
		}

		/// <summary>Gets or sets whether to start playing the challenge as soon as the page has loaded.</summary>
		[Category("Behavior")]
		[Description("Whether to start playing the challenge as soon as the page has loaded.")]
		[DefaultValue(AUTOSTART_DEFAULT)]
		public bool AutoStartChallenge
		{
			get { return _autoStart; }
			set { _autoStart = value; }
		}

		/// <summary>Gets or sets whether to display the Play button.</summary>
		[Category("Appearance")]
		[Description("Whether to display the \"Play\" button.")]
		[DefaultValue(SHOWPLAY_DEFAULT)]
		public bool ShowPlayButton
		{
			get { return _showPlayButton; }
			set { _showPlayButton = value; }
		}

		/// <summary>Gets or sets whether to spell out words rather than pronouncing them whole.</summary>
		[Category("Behavior")]
		[Description("Whether to spell out words rather than pronouncing them whole.")]
		[DefaultValue(SPELLWORDS_DEFAULT)]
		public bool SpellWords
		{
			get { return _spellWords; }
			set { _spellWords = value; }
		}

		/// <summary>Gets or sets the text to display on the \"Play\" button.</summary>
		[Category("Appearance")]
		[Description("The text to display on the \"Play\" button.")]
		[DefaultValue(TEXT_DEFAULT)]
		public string Text
		{
			get { return _text; }
			set 
			{
				if (value == null) throw new ArgumentNullException("Text");
				_text = value; 
			}
		}

		/// <summary>Selects the next word to be played.</summary>
		/// <returns>The next word to be played.</returns>
		protected override string ChooseWord()
		{
			// Get a word
			string word = base.ChooseWord();

			// If the user has opted to have words spelled rather than pronounced, generate
			// a string that contains the spelling and return that instead.
			if (_spellWords) 
			{
				char [] letters = word.ToCharArray();
				StringBuilder sb = new StringBuilder(letters.Length*3);
				foreach(char letter in letters)
				{
					int pos = (int)(Char.ToLower(letter) - 'a');
					if (pos >= 0 && pos < 26)
					{
						sb.Append(_spelledLetters[pos]); // use the pronounciation of the letter
						sb.Append("; "); // separator helps with speech timing
					}
				}
				return sb.ToString();
			}
				// Otherwise, just return the word
			else return word;
		}

		/// <summary>Render the challenge.</summary>
		/// <param name="id">The ID of the challenge.</param>
		/// <param name="content">The content to render.</param>
		protected override void RenderChallenge(Guid id, string content)
		{
			// Get the url to the audio
			string url = null;
			try
			{
				// If it's a valid URL, go with it.  Unfortunately, no easier way to parse it, and this
				// throws an exception on error.
				new Uri(RenderUrl);
				url = RenderUrl;
			}
			catch{}
			// If a fully-qualified URL wasn't supplied, treat what we have as relative
			if (url == null)
			{
				string appPath = Page.Request.ApplicationPath;
				url = Page.Request.Url.GetLeftPart(UriPartial.Authority) +
					appPath + (appPath.Length > 0 ? "/" : "") + RenderUrl + "?" + 
					ID_KEY + "=" + id.ToString("N");
			}

			// Add the WMP player control to the output
			string wmpId = "wmp" + Guid.NewGuid().ToString("N");
			HtmlGenericControl player = new HtmlGenericControl("object");
			player.Attributes["ID"] = wmpId;
			player.Attributes["CLASSID"] = "CLSID:6BF52A52-394A-11d3-B153-00C04F79FAA6";
			player.Attributes["height"] = "1";
			player.Attributes["width"] = "1";
			player.InnerHtml = 
				"<PARAM name=\"URL\" value=\"" + url + "\">" +
				"<PARAM name=\"autoStart\" value=\"" + _autoStart + "\">";
			Controls.Add(player);

			// Add a button to play the sound
			if (_showPlayButton)
			{
				Button playButton = new Button();
				if (!this.Width.IsEmpty) playButton.Width = this.Width;
				if (!this.Height.IsEmpty) playButton.Height = this.Height;
				playButton.Text = Text;
				playButton.EnableViewState = false;
				playButton.CausesValidation = false;
				playButton.Attributes["OnClick"] = wmpId + ".controls.play(); return false;"; // play but don't post back
				Controls.Add(playButton);
			}
		}
	}
}
