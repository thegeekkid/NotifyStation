<?php
	require_once('../config.inc');
	require_once('../rate_limit.inc');
	if ((isset($_GET['id']))) {

		require_once('../filters.inc');


		$id = hash('sha256', $_GET['id']);

		$sql = "SELECT call_active FROM computers WHERE id = ?;";
		$stmt = $mysqli->prepare($sql);
		$stmt->bind_param("s", $id);
		$stmt->execute();
		$result = $stmt->get_result();
		$active = '0';
		while($row = $result->fetch_assoc()) {
			$active = filterInts($row['call_active']);
		}
		if ($active === '1') {
			$sql = "UPDATE computers SET call_active = '0' WHERE id = ?";
			$stmt = $mysqli->prepare($sql);
			$stmt->bind_param("s", $id);
			$stmt->execute();
			echo "Active=True";
		}else {
			echo "Active=False";
		}
	}else {
		die('Unauthorized');
	}