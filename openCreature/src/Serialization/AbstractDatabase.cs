using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Metadata.W3cXsd2001;

namespace opencreature {
    public abstract class AbstractDatabase {
        const string PREFIX = "Database";
        
        public static log4net.ILog log;
        static AbstractDatabase() {
            log = log4net.LogManager.GetLogger(PREFIX);
        }
	
        
        public abstract List<List<Dictionary<string, string>>> sql(string query);
        public abstract List<List<Dictionary<string,string>>> this[string query] {get;}
        public abstract string getError();
        
        
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
                bits[i] = getBitFromBlob(blob, i);
            }
            return bits;
        }
	
        public static string getBlobFromBits(bool[] bits) {
            byte[] blob = (from x in bits select x ? (byte)0x1 : (byte)0x0).ToArray();
            string blobhex = (new SoapHexBinary(blob)).ToString();
            return blobhex;
        }
    }
}
