<!DOCTYPE html>
<html>
<head>
	<meta charset="utf-8" />
	<title>{Title}</title>
	<link rel="stylesheet" type="text/css" href="{~}/Styles/Main.css" />
</head>
<body>
	<script type="text/javascript" src="Scripts/jquery-2.1.1.min.js"></script>

	<div class="Header">
		<div class="Title">Your ACSP.NET application</div>
		<div class="Menu">
			<p class="Left">{SV:SiteUrl}</p>
			<p class="Right"><a href="{~}/">Home</a> <a href="about">About</a><a href="messagePageExample">Message page example</a></p>
		</div>
	</div>

	{MainContent}

	<div class="GenerationTimeFooter">{LabelExecutionTime}: {SV:SiteExecutionTime}</div>
</body>
</html>
