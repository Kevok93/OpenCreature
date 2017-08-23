using UnityEngine;
using System;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Metadata.W3cXsd2001;

public class Sqlite {
	private IntPtr db;
	static IntPtr null_ptr = (IntPtr)0;
	static char[] delim_semi = {';'};

	public Sqlite(string path, SqliteOpenOpts mode = SqliteOpenOpts.SQLITE_OPEN_READONLY) {
		SqliteErrorCode retc = sqlite3_open_v2(
			Encoding.Default.GetBytes(path), 
			ref db,
			mode,
			null
		);
		Debug.Log ("Sqlite3_open("+path+") = "+retc);
		if (retc != SqliteErrorCode.SQLITE_OK) db = null_ptr;
	}
	
	public List<List<Dictionary<string,string>>> sql(string query) {
		if (db == null_ptr) return null;
		//Debug.Log (db);
		List<List<Dictionary<string,string>>> results = new List<List<Dictionary<string, string>>>();
		SqliteErrorCode retc;
		
		string[] querytok = query.Split(delim_semi,StringSplitOptions.RemoveEmptyEntries);
		foreach (string subquery in querytok) {
			string subquery_mod = subquery + ";";
			IntPtr prep_stmt = null_ptr,
			leftovers = null_ptr;
			if (
				(retc = sqlite3_prepare_v2(
					db,
					Encoding.Default.GetBytes(subquery_mod),
					subquery_mod.Length,
					ref prep_stmt,
					ref leftovers
				)) == SqliteErrorCode.SQLITE_OK
			) {
			
				Debug.Log ("Sqlite3_prepare('"+subquery_mod+"') = " + retc);
				List<Dictionary<string,string>> resultset = new List<Dictionary<string,string>>();
				results.Add (resultset);
				#region row
				while (
					(retc = sqlite3_step (prep_stmt)) == SqliteErrorCode.SQLITE_ROW
				) {
					//Debug.Log ("Sqlite3_step('"+subquery_mod+"') = " + retc);
					Dictionary<string,string> row = new Dictionary<string, string>();
					resultset.Add (row);
					
					//Debug.Log ("Sqlite3_column_count('"+subquery_mod+"') = " + sqlite3_column_count(prep_stmt));
					
					int i = 0;
					for (; i < sqlite3_column_count(prep_stmt); i++) {
						IntPtr key_utf8 = sqlite3_column_name(prep_stmt,i);
						string key = Marshal.PtrToStringAnsi(key_utf8);
						string val = "ERROR";
						if (sqlite3_column_type(prep_stmt,i) == SqliteDatatype.SQLITE_BLOB) {
							IntPtr val_blob = sqlite3_column_text(prep_stmt,i);
							int size = 1;
							byte[] blob = new byte[size];
							Marshal.Copy(val_blob, blob, 0, size);
							val = (new SoapHexBinary(blob)).ToString();
						} else {
							IntPtr val_utf8 = sqlite3_column_text(prep_stmt,i);
							val = Marshal.PtrToStringAnsi(val_utf8);
						}
						//Debug.Log ("Sqlite3_column["+i+"]('"+key+"') = "+val);
						row.Add(key,val);
					}
					
				}
				//Debug.Log ("Sqlite3_step('"+subquery_mod+"') = " + retc);
				#endregion
				
				sqlite3_finalize (prep_stmt);
				sqlite3_finalize (leftovers);
			} else Debug.Log ("Sqlite3_prepare('"+subquery_mod+"') = " + retc);
		}
		return results;
	}
	
	public getBitFromBlob(string blob, int position) {
		int macro_pos = (position / 16) * 2;
		int micro_pos = pow(2,position % 8);
		string hexbyte = blob.substring(blob, macro_pos, 2);
		byte extractedByte = SoapHexBinary.Parse(hexbyte).Value[0];
		return ((extractedByte & micro_pos) > 0);
	}
	
	public List<List<Dictionary<string,string>>> this[string query] {
		get{return sql(query);}
	}
	
	public void Close() {
		Debug.Log (
			"Sqlite3_close() = "+
			sqlite3_close (db)
		);
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
	#region extern
	[DllImport ("sqlite3")]
	private static extern SqliteErrorCode sqlite3_open_v2(byte[] filename, ref IntPtr db, SqliteOpenOpts flags, byte[] VFS);

	[DllImport ("sqlite3")]
	private static extern SqliteErrorCode sqlite3_prepare_v2(IntPtr db, byte[] sql, int size, ref IntPtr statement, ref IntPtr leftovers);

	[DllImport ("sqlite3")]
	private static extern SqliteErrorCode sqlite3_finalize(IntPtr statement);

	[DllImport ("sqlite3")]
	private static extern SqliteErrorCode sqlite3_step(IntPtr statement);

	[DllImport ("sqlite3")]
	private static extern IntPtr sqlite3_column_text(IntPtr statement, int ColNum);

	[DllImport ("sqlite3")]
	private static extern IntPtr sqlite3_column_name(IntPtr statement, int ColNum);
	
	[DllImport ("sqlite3")]
	private static extern int sqlite3_column_count(IntPtr statement);
	
	[DllImport ("sqlite3")]
	private static extern SqliteErrorCode sqlite3_close(IntPtr db);
	
	[DllImport ("sqlite3")]
	private static extern SqliteDatatype sqlite3_column_type(IntPtr statement, int ColNum);
	
	#endregion
}