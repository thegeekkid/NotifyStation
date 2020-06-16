<?php
	require_once('../rate_limit.inc');
	if (isset($_GET['id'])) {
		require_once('../config.inc');
		require_once('../filters.inc');


		$id = hash('sha256', $_GET['id']);

		$sql = "UPDATE computers SET call_active = '1' WHERE id = ?";
		$stmt = $mysqli->prepare($sql);
		$stmt->bind_param("s", $id);
		$stmt->execute();

		echo "Success=True";
	}else {
		die("Unauthorized");
	}