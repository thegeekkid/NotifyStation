<?php

function filterInts($input) {

	$allowedChars = '1234567890';

	$result = "";
	$array = str_split($input);

	foreach ($array as $char) {
		if (strpos($allowedChars, $char) !== false) {
			$result = $result . $char;
		}
	}

	return $result;

}

function filterABC($input) {

	$allowedChars = 'abcdefghijklmnopqrstuvwxyz0123456789 ';

	$result = "";
	$array = str_split($input);

	foreach ($array as $char) {
		if (strpos($allowedChars, strtolower($char)) !== false) {
			$result = $result . $char;
		}
	}

	return $result;

}

function filterURL($input) {
	$allowedChars = 'abcdefghijklmnopqrstuvwxyz0123456789.-+=_/\\:%?&*@';

	$result = "";
	$array = str_split($input);

	foreach ($array as $char) {
		if (strpos($allowedChars, strtolower($char)) !== false) {
			$result = $result . $char;
		}
	}

	return $result;
}

function filterDate($input) {
	$allowedChars = 'abcdefghijklmnopqrstuvwxyz0123456789.-+=_/\\:%? ';

	$result = "";
	$array = str_split($input);

	foreach ($array as $char) {
		if (strpos($allowedChars, strtolower($char)) !== false) {
			$result = $result . $char;
		}
	}

	return $result;
}