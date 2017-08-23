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
		if (retc != SqliteErrorCode.SQLITE_OK) db = null_ptr;
	}
	
	public List<List<Dictionary<string,string>>> sql(string query) {
		if (db == null_ptr) return null;
		
		var results = new List<List<Dictionary<string, string>>>();
		
		string[] querytok = query.Split(delim_semi,StringSplitOptions.RemoveEmptyEntries);
		
		foreach (string subquery in querytok) {
			string subquery_mod = subquery + ";";
            Console.Out.WriteLine(subquery_mod);
            results.Add(getTable(subquery_mod));
		} 
					
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
	public List<Dictionary<string,string>> getTable(string sql) {
        int sizeofptr = Marshal.SizeOf(typeof(IntPtr));
        int cols, rows;
        IntPtr results_c;
        byte[] error_c;
        SqliteErrorCode retc;
        
        retc = sqlite3_get_table(
            db,
            Encoding.Default.GetBytes(sql),
            out results_c,
            out rows,
            out cols,
            out error_c
        );
        string[] keys = new string[cols];
        string error = (error_c != null) ? Encoding.Default.GetString(error_c) : "No Error";
        if (rows < 1) return new List<Dictionary<string, string>>(0);
        List<Dictionary<string,string>> result = new List<Dictionary<string, string>>(rows-1);
        for (int i = 0; i < cols; i++) {
            keys[i] = Marshal.PtrToStringAnsi(Marshal.ReadIntPtr(results_c, i * sizeofptr));
        }
        for (int i = cols; i < rows*cols+cols; i+=cols) {
            var row = new Dictionary<string,string>(cols);
            for (int j = 0; j < cols; j++) {
                row[keys[j]] = Marshal.PtrToStringAnsi(Marshal.ReadIntPtr(results_c, (i+j) * sizeofptr));
            }
            result.Add(row);
        }
        sqlite3_free_table(results_c);
        return result;
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

	[DllImport ("sqlite3")]
	public static extern SqliteErrorCode sqlite3_get_table(IntPtr db, byte[] sql, out IntPtr results, out int rows, out int cols, out byte[] err);
	
	[DllImport ("sqlite3")]
	public static extern void sqlite3_free_table(IntPtr tableResult);
	
	#endregion
}