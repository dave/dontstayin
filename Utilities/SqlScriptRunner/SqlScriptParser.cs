using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace SqlScriptRunner
{

    //Imports System.Collections.Generic
    internal class SqlScriptParser
    {
        //    'parameter values
        char[] scriptContents;
        bool AddWithEncryption;

        //    'initial values
        bool WithEncryptionAdded = false;
        int bracketCount = 0;
        int pos = -1;

        SqlScriptParser(string script, bool withEncryption){
            this.scriptContents = script.ToCharArray();
            this.AddWithEncryption = withEncryption;
            inSQLCode();
        }


        List<string> sections = new List<string>();
        StringBuilder currentSection = new StringBuilder();
        char c;
        bool IsCreateSection;

        internal static string[] Parse(string script, bool withEncryption) {
            SqlScriptParser parser = new SqlScriptParser(script, withEncryption);
            return parser.sections.ToArray();
        }

        //#Region "parser }s"

        string WithEncryptionInsertToken = "as";
        void inSQLCode()
        {
            do
            {

                c = NextChar();
                if (IsEndofScript())
                {
                    break;
                }
                if (AddWithEncryption && !IsCreateSection && TestForToken("create", true))
                {
                    IsCreateSection = true;
                }
                if (IsCreateSection && !WithEncryptionAdded && bracketCount == 0 && WithEncryptionInsertToken == "as")
                {
                    if (TestForToken("TRIGGER", true))
                    {
                        WithEncryptionInsertToken = "for";
                    }
                }
                if (IsCreateSection && !WithEncryptionAdded && bracketCount == 0)
                {
                    if (TestForToken(WithEncryptionInsertToken, true))
                    {
                        currentSection.Append(" WITH ENCRYPTION ");
                        WithEncryptionAdded = true;
                        WithEncryptionInsertToken = "as";
                    }
                }
                if (c == CharConstants.OpenBracket)
                {
                    bracketCount += 1;
                }
                if (c == CharConstants.CloseBracket)
                {
                    bracketCount -= 1;
                }

                if (EnteringComment())
                {
                    InComment();
                }
                else if (EnteringCommentBlock())
                {
                    InCommentBlock();
                }
                else if (c == CharConstants.Quote)
                {
                    InTextRegion(CharConstants.Quote, CharConstants.Quote);
                }
                else if (c == CharConstants.DoubleQuote)
                {
                    InTextRegion(CharConstants.DoubleQuote, CharConstants.DoubleQuote);
                }
                else if (c == CharConstants.OpenSquareBracket)
                {
                    InTextRegion(CharConstants.OpenSquareBracket, CharConstants.CloseSquareBracket);
                }
                else if (TestForToken("go", true))
                {
                    pos += 1;
                    if (currentSection.ToString().Trim().Length > 0)
                    {
                    	
						sections.Add(currentSection.ToString());
                    }
                    bracketCount = 0;
                    WithEncryptionAdded = false;
                    IsCreateSection = false;
                    currentSection = new StringBuilder();
                }
                else
                {
                    currentSection.Append(c);
                }
            } while (true);
            sections.Add(currentSection.ToString());
        }
                

            void  InTextRegion(char startChar, char endChar){
                currentSection.Append(c);
                do
                {
                    c = NextChar();
                    if (IsEndofScript()) { break; }
                    if (isdoubleExitCharacter(endChar))
                    {
                        currentSection.Append(endChar).Append(endChar);
                        c = NextChar();
                    }
                    else if (c == endChar)
                    {
                        currentSection.Append(c);
                        break;
                    }
                    else
                    {
                        currentSection.Append(c);
                    }
                } while (true);
            }
            void  InComment(){
                currentSection.Append(c);
                do
                {
                    c = NextChar();
                    if (IsEndofScript()) { 
                        break; 
                    }
                    currentSection.Append(c);
                    if (ExitingComment())
                    {
                        break;
                    }
                } while (true);
            }
            void  InCommentBlock(){
                currentSection.Append(c);
                do
                {
                    c = NextChar();
                    if (IsEndofScript())
                    {
                        break;
                    }
                    currentSection.Append(c);
                    if (exitingCommentBlock())
                    {
                        currentSection.Append(NextChar());
                        break;
                    }
                } while (true);
            }
        //#End Region
        static class CharConstants
        {
            internal const char ForwardSlash = '/';
            internal const char Asterisk = '*';
            internal const char Hypen = '-';
            internal const char Space = ' ';
            internal const char Quote = '\'';
            internal const char DoubleQuote = '"';
            internal const char OpenSquareBracket = '[';
            internal const char CloseSquareBracket = ']';
            internal const char OpenBracket = '(';
            internal const char CloseBracket = ')';
            internal const char CarriageReturn = '\r';
            internal const char LineFeed = '\n';



        }
        #region "parser functions"
        bool EnteringCommentBlock()
        {
            return c == CharConstants.ForwardSlash && peek(1) == CharConstants.Asterisk;
        }
        bool EnteringComment()
        {
            return c == CharConstants.Hypen && peek(1) == CharConstants.Hypen;
        }
        bool ExitingComment()
        {
            return c == CharConstants.CarriageReturn || c == CharConstants.LineFeed;
        }
        bool exitingCommentBlock()
        {
            return c == CharConstants.Asterisk && peek(1) == CharConstants.ForwardSlash;
        }
        bool isdoubleExitCharacter(char endChar)
        {
            return c == endChar && peek(1) == endChar;
        }

        bool TestForToken(string text, bool hasWhiteSpaceEitherSide)
        {
            char[] array = text.ToLower().ToCharArray();
            if (hasWhiteSpaceEitherSide)
            {
                if (!Char.IsWhiteSpace(peek(-1)) && !(peek(-2) == CharConstants.Asterisk && peek(-1) == CharConstants.ForwardSlash)) { return false; }
                if (!Char.IsWhiteSpace(peek(array.Length)) && !(peek(array.Length) == CharConstants.ForwardSlash && peek(array.Length + 1) == CharConstants.Asterisk)) { return false; }
            }
            //check letters are the same
            for (int i = 0; i < array.Length; i++)
            {
                if (Char.ToLower(peek(i)) != array[i])
                {
                    return false;
                }
            }
            return true;
        }
        char NextChar()
        {
             pos += 1;
            if (pos >= scriptContents.Length) { return CharConstants.Space; }
            return scriptContents[pos];
        }
        char peek(int amount)
        {
            if ((pos + amount) >= scriptContents.Length || pos + amount < 0)
            {
                return CharConstants.Space;
            }
            return scriptContents[pos + amount];
        }
        bool IsEndofScript()
        {
            return pos >= this.scriptContents.Length;
        }
        #endregion


    }


}
