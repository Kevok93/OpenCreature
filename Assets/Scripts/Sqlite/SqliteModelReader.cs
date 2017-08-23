using UnityEngine;
using System;
using System.Linq.Expressions;
using System.Linq;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Metadata.W3cXsd2001;

public static class SqliteModelReader {
	public static IntPtr null_ptr = (IntPtr)0;
	public static Mesh readMesh(string path) {
		IntPtr db = null_ptr;
		SqliteErrorCode retc = Sqlite.sqlite3_open_v2(
			Encoding.Default.GetBytes(path), 
			ref db,
			SqliteOpenOpts.SQLITE_OPEN_READONLY,
			null
		);
		if (db == null_ptr) return null;
		var watch = new System.Diagnostics.Stopwatch();
		watch.Start();
		Dictionary<int,Vector3> vertexList = readVerticies (db);
		Console.Out.WriteLine("Read Verticies: "+watch.ElapsedMilliseconds);
		watch.Reset(); watch.Start();
		List<int> polyList = readPolygons (db, vertexList);
		Console.Out.WriteLine("Read Polys: "+watch.ElapsedMilliseconds);
		watch.Stop();
		if (db != null_ptr) Sqlite.sqlite3_close (db);
		Mesh mesh = new Mesh ();
		mesh.vertices = vertexList.Values.ToArray();
		mesh.triangles = polyList.ToArray();
		return mesh;

	}
	public static Dictionary<int,Vector3> readVerticies(IntPtr db) {
		string query = "Select * from vertices;"; //TODO: Fix me
		IntPtr prep_stmt = null_ptr,
		leftovers = null_ptr;
		SqliteErrorCode retc = Sqlite.sqlite3_prepare_v2 (
			db,
			Encoding.Default.GetBytes (query),
			query.Length,
			ref prep_stmt,
			ref leftovers
		);
		string retc_string = retc.ToString();
		Dictionary<int,Vector3> vertexList = null;
		if (retc == SqliteErrorCode.SQLITE_OK) {
			vertexList = new Dictionary<int,Vector3> ();
			while (true) {
			    retc = Sqlite.sqlite3_step (prep_stmt);
			    if (retc == SqliteErrorCode.SQLITE_ROW) break;
				int	i=Sqlite.sqlite3_column_int(prep_stmt,0),
					x=Sqlite.sqlite3_column_int(prep_stmt,1),
					y=Sqlite.sqlite3_column_int(prep_stmt,2),
					z=Sqlite.sqlite3_column_int(prep_stmt,3);
				vertexList[i] = new Vector3(x,y,z);
			}
			Sqlite.sqlite3_finalize (prep_stmt);
			Sqlite.sqlite3_finalize (leftovers);
		}

		return vertexList;
	}
	public static List<int> readPolygons(IntPtr db, Dictionary<int,Vector3> vertexList) {
		string query = "Select * from polygons;";
		IntPtr prep_stmt = null_ptr,
		leftovers = null_ptr;
		SqliteErrorCode retc = Sqlite.sqlite3_prepare_v2 (
			db,
			Encoding.Default.GetBytes (query),
			query.Length,
			ref prep_stmt,
			ref leftovers
			);
		List<int> polyList = null;

		if (retc == SqliteErrorCode.SQLITE_OK) {
			polyList = new List<int> ();
			while (
				(retc = Sqlite.sqlite3_step (prep_stmt)) == SqliteErrorCode.SQLITE_ROW
			) {
				int	c1=Sqlite.sqlite3_column_int(prep_stmt,0),
					c2=Sqlite.sqlite3_column_int(prep_stmt,1),
					c3=Sqlite.sqlite3_column_int(prep_stmt,2);
				if (
					vertexList.ContainsKey (c1) &&
					vertexList.ContainsKey (c2) &&
					vertexList.ContainsKey (c3)
				) {
					polyList.Add (c1);
					polyList.Add (c2);
					polyList.Add (c3);
				} else return null;
			}
			Sqlite.sqlite3_finalize (prep_stmt);
			Sqlite.sqlite3_finalize (leftovers);
		}

		return polyList;
	}

}
