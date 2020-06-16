<?php
	require_once('vendor/autoload.php');
	use bandwidthThrottle\tokenBucket\Rate;
	use bandwidthThrottle\tokenBucket\TokenBucket;
	use bandwidthThrottle\tokenBucket\storage\FileStorage;

	$storage = new FileStorage(__DIR__ . "/api.bucket.prod");
	$rate    = new Rate(50, Rate::SECOND);
	$bucket  = new TokenBucket(50, $rate, $storage);
	//$bucket->bootstrap(50);

	if (!$bucket->consume(1, $seconds)) {
		http_response_code(429);
		header(sprintf("Retry-After: %d", floor($seconds)));
		exit();
	}