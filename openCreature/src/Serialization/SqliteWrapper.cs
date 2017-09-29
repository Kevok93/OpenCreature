using System;
using System.Collections.Generic;
using Mono.Data.Sqlite;
using System.Linq;
using System.Runtime.Remoting.Metadata.W3cXsd2001;

namespace opencreature {
    public class SqliteWrapper {
        static log4net.ILog log = log4net.LogManager.GetLogger("SqliteWrapper");
        SqliteConnection dbh;
        
        public SqliteWrapper(String connect_string) {
            log.Debug("Attemting to open logfile "+connect_string);
            dbh = new SqliteConnection(connect_string);
            dbh.Open();
        }

        public List<Dictionary<string,string>> simpleQuery(string query) {
            SqliteCommand cmd = new SqliteCommand(query,dbh);
            var reader = cmd.ExecuteReader();
            
            List<Dictionary<string,string>> results = new List<Dictionary<string, string>>();
            while (reader.Read()) {
                Dictionary<string,string> row = new Dictionary<string, string>();
                for (int i = 0; i < reader.FieldCount; i++) try {
                    row.Add(
                        reader.GetName(i),
                        reader.GetFieldType(i).IsArray
                            ? new SoapHexBinary((byte[])reader.GetValue(i)).ToString()
                            : reader.GetValue(i).ToString()
                    );
                } catch (Exception e) {
                    log.ErrorFormat(
                        "Error converting row to string. {0}'s type is {1}.",
                        reader.GetName(i), 
                        reader.GetFieldType(i)
                    );
                    throw;
                }
                results.Add(row);
            }
            return results;
        }

        public List<Dictionary<string,string>> this[string query] {
            get {
                return simpleQuery(query);
            }
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
            int size = blob.Length * 4;
            bool[] bits = new bool[size];
            for (int i = 0; i < size; i++) {
                bits[i] = getBitFromBlob(blob, i);
            }
            return bits;
        }
	
        public static string getBlobFromBits(bool[] bits) {
            byte[] blob = (from x in bits select x ? (byte)0x1 : (byte)0x0).ToArray();
            string blobhex = new SoapHexBinary(blob).ToString();
            return blobhex;
        }
    }
    
}
