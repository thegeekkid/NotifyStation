<?php

$SQLhost = 'localhost';
$SQLdb = 'DB_NAME';
$SQLusr = 'DB_USER';
$SQLpw = 'DB_USER_PASSWORD';

$mysqli = new mysqli($SQLhost, $SQLusr, $SQLpw, $SQLdb);

$debug = false;

if ($debug) {
	ini_set('display_errors', 1);
	ini_set('display_startup_errors', 1);
	error_reporting(E_ALL);
}