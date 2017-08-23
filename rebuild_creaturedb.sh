sqlite ":memory:" ".read db_sql_statements/creature.db.schema.sql" ".read db_sql_statements/creature.db.values.sql" ".save creature.db"
cp creature.db Temp/bin/Debug/
cp creature.db bin/Debug/
cp creature.db obj/Debug/
