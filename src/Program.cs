﻿// Gzip.exe compresses files using the Gzip compression algorithm.
// Copyright( C ) 2023 Timothy J. Bruce

/*
    This program is free software: you can redistribute it and/or modify
    it under the terms of the GNU General Public License as published by
    the Free Software Foundation, either version 3 of the License, or
    (at your option) any later version.

    This program is distributed in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
    GNU General Public License for more details.

    You should have received a copy of the GNU General Public License
    along with this program.  If not, see <https://www.gnu.org/licenses/>.
*/
using System.Linq;
using static System.Net.WebRequestMethods;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Xml.Linq;

namespace Icod.Gzip {

	public static class Program {

		[System.STAThread]
		public static System.Int32 Main( System.String[] args ) {
			if ( null == args ) {
				PrintUsage();
				return 1;
			}
			var len = args.Length;
			if ( len <= 0 ) {
				PrintUsage();
				return 1;
			} else if ( 2 < len ) {
				PrintUsage();
				return 1;
			}
			System.String inputPathName = args[ 0 ];
			if (
				( "--help" == inputPathName )
				|| ( "-h" == inputPathName )
				|| ( "/help" == inputPathName )
			) {
				PrintUsage();
				return 1;
			} else if (
				( "--copyright" == inputPathName )
				|| ( "-c" == inputPathName )
				|| ( "/c" == inputPathName )
			) {
				PrintCopyright();
				return 1;
			}
			System.String outputPathName;
			if (2 <= args.Length ) {
				outputPathName = args[ 1 ];
				if ( System.IO.Directory.Exists( outputPathName ) ) {
					outputPathName = System.IO.Path.Combine( outputPathName, System.IO.Path.GetFileName( inputPathName ) ) + ".gzip";
				}
			} else {
				outputPathName = System.IO.Path.Combine( System.Environment.CurrentDirectory, System.IO.Path.GetFileName( inputPathName ) + ".gzip" );
			}
			var output = 0;
			try {
				Compress( inputPathName, outputPathName );
			} catch ( System.Exception e ) {
				System.Console.Error.WriteLine( e.Message );
				output = 1;
			}
			return output;
		}

		private static void Compress( System.String inputFilePathName, System.String outputFilePathName ) {
#if DEBUG
			if ( System.String.IsNullOrEmpty( inputFilePathName ) ) {
				throw new System.ArgumentNullException( "inputFilePathName" );
			} else if ( System.String.IsNullOrEmpty( outputFilePathName ) ) {
				throw new System.ArgumentNullException( "outputFilePathName" );
			}
#endif
			using ( var reader = System.IO.File.OpenRead( inputFilePathName ) ) {
				using ( var output = System.IO.File.OpenWrite( outputFilePathName ) ) {
					using ( var worker = new System.IO.Compression.GZipStream( output, System.IO.Compression.CompressionMode.Compress, true ) ) {
						reader.CopyTo( worker );
						worker.Flush();
						worker.Close();
					}
					output.Flush();
					output.SetLength( output.Position );
					output.Close();
				}
				reader.Close();
			}
		}

		private static void PrintUsage() {
			System.Console.Error.WriteLine( "No, no, no! Use it like this, Einstein:" );
			System.Console.Error.WriteLine( "Gzip.exe --help" );
			System.Console.Error.WriteLine( "Gzip.exe --copyright" );
			System.Console.Error.WriteLine( "Gzip.exe inputFilePathName [outputFilePathNam]" );
			System.Console.Error.WriteLine( "inputFilePathName and outputFilePathName may be relative or absolute paths." );
			System.Console.Error.WriteLine( "If outputFilePathName is omitted then it will be generated in the current directory and named the same as inputFilePathName but with '.gzip' appended to the end." );
			System.Console.Error.WriteLine( "If the outputFilePathName is an existing directory, then it will be generated in that directory and named the same as inputFilePathName but with '.gzip' appended to the end." );
		}
		private static void PrintCopyright() {
			var copy = new System.String[] {
				"Gzip.exe compresses files using the Gzip compression algorithm.",
				"",
				"Copyright( C ) 2023 Timothy J. Bruce",
				"",
				"This program is free software: you can redistribute it and / or modify",
				"it under the terms of the GNU General Public License as published by",
				"the Free Software Foundation, either version 3 of the License, or",
				"( at your option ) any later version.",
				"",
				"",
				"This program is distributed in the hope that it will be useful,",
				"but WITHOUT ANY WARRANTY; without even the implied warranty of",
				"",
				"MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.See the",
				"",
				"GNU General Public License for more details.",
				"",
				"",
				"You should have received a copy of the GNU General Public License",
				"",
				"along with this program.If not, see < https://www.gnu.org/licenses/>."
			};
			foreach ( var line in copy) {
				System.Console.WriteLine( line );
			}
		}

	}

}