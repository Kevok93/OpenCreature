using UnityEngine;
using System;
using System.Linq.Expressions;
using System.Linq;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Metadata.W3cXsd2001;

public class Sqlite {
	protected IntPtr db;
	public static IntPtr null_ptr = (IntPtr)0;
	public static char[] delim_semi = {';'};

	public Sqlite(string path, SqliteOpenOpts mode = SqliteOpenOpts.SQLITE_OPEN_READONLY) {
		SqliteErrorCode retc = sqlite3_open_v2(
			Encoding.Default.GetBytes(path), 
			ref db,
			mode,
			null
		);
        //Debug.Log ("Sqlite3_open("+path+") = "+retc);
        Debug.
		if (retc != SqliteErrorCode.SQLITE_OK) db = null_ptr;
	}
	
	public List<List<Dictionary<string,string>>> sql(string query) {
	    //System.Diagnostics.Stopwatch watch1 = new System.Diagnostics.Stopwatch();
	    //System.Diagnostics.Stopwatch watch2 = new System.Diagnostics.Stopwatch();
		if (db == null_ptr) return null;
		
		var results = new List<List<Dictionary<string, string>>>();
		SqliteErrorCode retc;
		
		string[] querytok = query.Split(delim_semi,StringSplitOptions.RemoveEmptyEntries);
		
		foreach (string subquery in querytok) {
			string subquery_mod = subquery + ";";
			
			IntPtr 
			    prep_stmt = null_ptr,
			    leftovers = null_ptr;
			    
            retc = sqlite3_prepare_v2(
                db,
                Encoding.Default.GetBytes(subquery_mod), 
                subquery_mod.Length,
                ref prep_stmt, 
                ref leftovers
            );
            
			if (retc == SqliteErrorCode.SQLITE_OK) {
			
				var resultset = new List<Dictionary<string,string>>();
				results.Add (resultset);
				
				int col_count = sqlite3_column_count(prep_stmt);
                string[] keys = new string[col_count];
                SqliteDatatype[] types = new SqliteDatatype[col_count];
				for (int i = 0; i < col_count; i++) {
				    IntPtr key_utf8 = sqlite3_column_name(prep_stmt,i);
				    keys[i] = Marshal.PtrToStringAnsi(key_utf8);
				    types[i] = sqlite3_column_type(prep_stmt,i);
				}
				
				#region row
				while (true) {
				    retc = sqlite3_step (prep_stmt);
				    if (retc != SqliteErrorCode.SQLITE_ROW) break;
				    
					var row = new Dictionary<string, string>();
					resultset.Add (row);
					
					for (int i = 0; i < col_count; i++) {
						string val = "ERROR";
						
						if (types[i] == SqliteDatatype.SQLITE_BLOB) {
							IntPtr val_blob = sqlite3_column_blob(prep_stmt,i);
                            int size = sqlite3_column_bytes(prep_stmt,i);
							byte[] blob = new byte[size];
							Marshal.Copy(val_blob, blob, 0, size);
							//Debug.Log ("Sqlite3_column["+i+"]('"+key+"') = Blob["+size+"]("+blob+")");
							val = (new SoapHexBinary(blob)).ToString();
						} else {
							IntPtr val_utf8 = sqlite3_column_text(prep_stmt,i);
                            val = Marshal.PtrToStringAnsi(val_utf8);
						}
						
						row.Add(keys[i],val);
					}
				}
				#endregion
				
				sqlite3_finalize (prep_stmt);
				sqlite3_finalize (leftovers);
		    } else {
                IntPtr 
                    err_msg_utf8 = sqlite3_errmsg(db),
                    err_str_utf8 = sqlite3_errstr(db);
                string 
                    err_msg = Marshal.PtrToStringAnsi(err_msg_utf8),
                    err_str = Marshal.PtrToStringAnsi(err_str_utf8);
                Console.Error.WriteLine("Error in sql: " + retc + "\n" + err_msg + err_str);
		    } 
		} //End foreach query
		//Console.Out.WriteLine(string.Format("Parsing type: {0} milliseconds", watch1.ElapsedMilliseconds) );
		//Console.Out.WriteLine(string.Format("Parsing value: {0} milliseconds", watch2.ElapsedMilliseconds) );
					
		return results;
	}
	
	public List<List<Dictionary<string,string>>> this[string query] {
		get{return sql(query);}
	}
	
	public void Close() {
		//Debug.Log (
		//	"Sqlite3_close() = "+
		//	sqlite3_close (db)
		//);
		db = null_ptr;
	}

	~Sqlite() {
		if (db != null_ptr) Close ();
	}

	public static string printResultSet(List<List<Dictionary<string,string>>> resultSet) {
		string output = "";
		foreach (List<Dictionary<string,string>> result in resultSet) {
			output += "Result: {\n";
			output += printResult(result);
			output += "}\n";
		}
		return output;
	}

	public static string printResult(List<Dictionary<string,string>> resultSet) {
		string output = "";
		foreach (Dictionary<string,string> result in resultSet) {
			output += "\tRow: [\n";
			output += printResultRow(result);
			output += "\t]\n";
		}
		return output;
	}

	public static string printResultRow(Dictionary<string,string> row) {
		string output = "";
		foreach (string key in row.Keys) {
			output += "\t\t" + key + " = " + row[key] + "\n";
		}
		return output;
	}

	public static bool getBitFromBlob(string blob, int position) {
		int size = blob.Length * 4;
		int realPos = size - position - 1;
		int macro_pos = (realPos / 8) * 2;
		int micro_pos = 1 << 7-(realPos % 8);
		string hexbyte = blob.Substring(macro_pos, 2);
		byte extractedByte = SoapHexBinary.Parse(hexbyte).Value[0];
		bool bit = ((extractedByte & micro_pos) > 0);
		//Debug.Log("TM"+(position+1)+" = "+bit+" ["+hexbyte+"]");
		return bit;
	}
	
	public static bool[] getBitsFromBlob(string blob) {
        //return new bool[] { };
		int size = blob.Length * 4;
		bool[] bits = new bool[size];
		for (int i = 0; i < size; i++) {
			bits[i] = Sqlite.getBitFromBlob(blob, i);
		}
		return bits;
	}
	
	public static string getBlobFromBits(bool[] bits) {
        byte[] blob = (from x in bits select x ? (byte)0x1 : (byte)0x0).ToArray();
        string blobhex = (new SoapHexBinary(blob)).ToString();
        return blobhex;
	}

	#region extern
	[DllImport ("sqlite3")]
	public static extern SqliteErrorCode sqlite3_open_v2(byte[] filename, ref IntPtr db, SqliteOpenOpts flags, byte[] VFS);

	[DllImport ("sqlite3")]
	public static extern SqliteErrorCode sqlite3_prepare_v2(IntPtr db, byte[] sql, int size, ref IntPtr statement, ref IntPtr leftovers);

	[DllImport ("sqlite3")]
	public static extern SqliteErrorCode sqlite3_finalize(IntPtr statement);

	[DllImport ("sqlite3")]
	public static extern SqliteErrorCode sqlite3_step(IntPtr statement);

	[DllImport ("sqlite3")]
	public static extern IntPtr sqlite3_column_blob(IntPtr statement, int ColNum);

	[DllImport ("sqlite3")]
	public static extern IntPtr sqlite3_column_text(IntPtr statement, int ColNum);

	[DllImport ("sqlite3")]
	public static extern int sqlite3_column_bytes(IntPtr statement, int ColNum);

	[DllImport ("sqlite3")]
	public static extern IntPtr sqlite3_column_name(IntPtr statement, int ColNum);

	[DllImport ("sqlite3")]
	public static extern int sqlite3_column_count(IntPtr statement);
	
	[DllImport ("sqlite3")]
	public static extern SqliteErrorCode sqlite3_close(IntPtr db);
	
	[DllImport ("sqlite3")]
	public static extern SqliteDatatype sqlite3_column_type(IntPtr statement, int ColNum);

	[DllImport ("sqlite3")]
	public static extern int sqlite3_column_int(IntPtr statement, int ColNum);

	[DllImport ("sqlite3")]
	public static extern float sqlite3_column_float(IntPtr statement, int ColNum);

	[DllImport ("sqlite3")]
	public static extern IntPtr sqlite3_errmsg(IntPtr db);

	[DllImport ("sqlite3")]
	public static extern IntPtr sqlite3_errstr(IntPtr db);
	
	#endregion
}