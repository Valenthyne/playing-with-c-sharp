/*
 * Generic C# Lexical Analyzer
 * Based Closely off front.c (C-based) from the textbook
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LexicalAnalysisExercise
{
	class LexAnalyzer
	{
		
		// Static Variables to be used for later
		static int charClass;
		static char[] lexeme = new char[100];
		static char nextChar;
		static int lexLen;
		static int nextToken;
		static int totalToken = 0;

		// String that will be received from the console for purpose of lexical analysis
		static String input = "";

		// Index to keep track of location of characters in the input string
		static int index;

		// Classes for particular characters that will be important for if they're concatenated
		const int LETTER = 0;
		const int DIGIT = 1;
		const int UNKNOWN = 99;
		
		// Token codes that will be used to identify individual tokens when determined
		const int INT_LIT = 10; // integer literals
		const int IDENT = 11; // any variable name
		const int ASSIGN_OP = 20; // = sign
		const int ADD_OP = 21; // + sign
		const int SUB_OP = 22; // - sign
		const int MULT_OP = 23; // * sign
		const int DIV_OP = 24; // / sign
		const int LEFT_PAREN = 25; // ( left parenthesis
		const int RIGHT_PAREN = 26; // ) right parenthesis
		const int LESS_THAN = 27; // < sign
		const int GREATER_THAN = 28; // > sign
		const int EOI = -1; // input has terminated; no more to analyze

		static void Main(string[] args)
		{

				// Prompts use to receive input to analyze
				Console.Write("Type an expression to analyze: ");
				input = Console.ReadLine();

				// Retrieves the first character of the string and orchestrates a do-while loop to cycle through the input
				getChar();
				do
				{
					lex();
				} while (nextToken != EOI); // When EOF (end-of-file) is reached, the analysis will be complete)

				// Shows the total number of tokens evaluated (inclusive of the EOI token)
				Console.WriteLine("Total Tokens: " + totalToken);

				// Prompt to read key so the program doesn't immediately terminate
				Console.ReadKey();

		}

		// Method using a switch statement to evaluate to which token a given character belongs
		static int lookup(char ch)
		{
			switch (ch)
			{
				case '(':
					addChar();
					nextToken = LEFT_PAREN;
					break;
				case ')':
					addChar();
					nextToken = RIGHT_PAREN;
					break;
				case '+':
					addChar();
					nextToken = ADD_OP;
					break;
				case '-':
					addChar();
					nextToken = SUB_OP;
					break;
				case '*':
					addChar();
					nextToken = MULT_OP;
					break;
				case '/':
					addChar();
					nextToken = DIV_OP;
					break;
				case '=':
					addChar();
					nextToken = ASSIGN_OP;
					break;
				case '<':
					addChar();
					nextToken = LESS_THAN;
					break;
				case '>':
					addChar();
					nextToken = GREATER_THAN;
					break;
				default:
					addChar();
					nextToken = EOI;
					break;
			}
			// Returns the token class identifier 
			return nextToken;
		}
		
		// Method used to add the subsequent character to the lexeme array, assuming it isn't too long
		static void addChar()
		{
			if (lexLen <= 98)
			{
				lexeme[lexLen++] = nextChar;
				lexeme[lexLen] = '0';
			}
			else
				Console.WriteLine("Error - lexeme is too long");
		}
		
		// Method to receive the next character in the input and act accordingly based on what it is
		static void getChar()
		{
			if (index < input.Length)
			{
				// Increments index in file to ensure it works properly
				nextChar = input[index++];

				// If-else chain to validate whether it's a letter, digit, or unknown (symbol likely)
				if (Char.IsLetter(nextChar))
				{
					charClass = LETTER;
				}
				else if (Char.IsDigit(nextChar))
				{
					charClass = DIGIT;
				}
				else
				{
					charClass = UNKNOWN;
				}
			}
			else
			{
				charClass = EOI;
			}
		}
	
		// Method that will skip whitespace characters for purpose of the lexical analysis
		static void getNonBlank()
		{
			while (Char.IsWhiteSpace(nextChar))
				getChar();
		}
		
		// Primary method that will analyze the nature of a given lexeme and return appropriate token identification
		static int lex()
		{
			lexLen = 0;
			getNonBlank();
			totalToken++;
			switch (charClass)
			{
				// Variable names and identifiers will be caught here since received input will be a concatenation of letters and/or digits 
				case LETTER:
					addChar();
					getChar();
					while (charClass == LETTER || charClass == DIGIT)
					{
						addChar();
						getChar();
					}
					nextToken = IDENT;
					break;
				
				// Integer literals will be caught since received input will be a concatenation of digits
				case DIGIT:
					addChar();
					getChar();
					while (charClass == DIGIT)
					{
						addChar();
						getChar();
					}
					nextToken = INT_LIT;
					break;
				
				// Parantheses, operators, and other symbols will likely be caught here.
				case UNKNOWN:
					lookup(nextChar);
					getChar();
					break;
				
				// Case for when the end-of-input market will be caught
				case EOI:
					nextToken = EOI;
					lexeme[0] = 'E';
					lexeme[1] = 'O';
					lexeme[2] = 'I';
					lexeme[3] = '0';
					break;
			} 

			// C# convention using String constructor to correctly post identifiers for the token and lexeme
			Console.WriteLine("Next token: {0}, Next lexeme: {1}", nextToken, new string(lexeme, 0, Array.IndexOf(lexeme, '0')));
			return nextToken;
		} 
	}
}