#!/bin/perl
use Data::Dumper;
use Getopt::Long qw(:config pass_through no_ignore_case bundling); # local option handling
#use Sys::OOSyslog qw(:ALL); # syslog constants like LOG_ERR ...
use POSIX;
use Archive::Tar;
use Time::HiRes qw(usleep time);
use File::Compare;
use File::Temp (tempfile, tempdir);

my $db_path;
my $cols = `tput cols`;
$cols += 0;
if (scalar(@ARGV)) {
    GetOptions("path=s"=>\$db_path);
}
die "Please supply --path to creature repository" unless ($db_path);

my ($local_cdb_fh , $local_cdb_name ) = tempfile();
my ($master_cdb_fh, $master_cdb_name) = tempfile();

print $local_cdb_fh  "WORKING\n";
print $local_cdb_fh  `sqlite3 $db_path/creature.db ".dump"`;

print $master_cdb_fh "MASTER\n";
print $master_cdb_fh `sqlite3  ":memory:" ".read $db_path/db_sql_statements/creature.db.schema.sql" ".read $db_path/db_sql_statements/creature.db.values.sql" ".dump"`;

system("diff -ywW $cols --suppress-common-lines $local_cdb_name $master_cdb_name");

unlink $local_cdb_name, $master_cdb_name;
