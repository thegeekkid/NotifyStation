<?php
	require_once('../config.inc');
	require_once('../filters.inc');
	require_once('../rate_limit.inc');

	$id = hash('sha256', random_bytes(64));
	$storID = hash('sha256', $id);

	$sql = "INSERT INTO computers(id) VALUES (?)";
	$stmt = $mysqli->prepare($sql);
	$stmt->bind_param("s", $storID);
	$stmt->execute();

	echo "ID=" . $id;