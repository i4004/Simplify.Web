<!DOCTYPE html>
<html>
<head>
	<meta charset="utf-8" />
	<title>{Title}</title>
	<link rel="stylesheet" type="text/css" href="http://localhost/AcspNet.Examples/Styles/Main.css" />
</head>
<body>
	<div id="header">
		<h1>Your ACSP.NET application</h1>
		<div class="menu">
			<p class="left">{SV:SiteUrl}</p>
			<p class="right"><a href="{~}/">Home</a> <a href="about">About</a><a href="messagePageExample">Message page example</a></p>
		</div>
	</div>

	{MainContent}

	<div class="GenerationTimeFooter">{LabelGeneratedIn}: {SV:SiteExecutionTime}</div>
</body>
</html>
