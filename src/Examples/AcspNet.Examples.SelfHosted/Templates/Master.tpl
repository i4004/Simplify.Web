<!DOCTYPE html>
<html>
<head>
	<meta charset="utf-8" />
	<meta name="viewport" content="width=device-width, initial-scale=1.0">
	<title>{Title}</title>
	<link rel="stylesheet" type="text/css" href="{~}/Content/bootstrap.min.css" />
	<link rel="stylesheet" type="text/css" href="{~}/Content/bootstrap-theme.css" />
	<link rel="stylesheet" type="text/css" href="{~}/Styles/Main.css" />
</head>
<body>
	<script type="text/javascript" src="{~}/Scripts/jquery-2.1.1.min.js"></script>
	<script type="text/javascript" src="{~}/Scripts/bootstrap.min.js"></script>

	<div class="Title"><img class="Logo" src="{~}/Images/Icon.png" alt="AcspNet" />Your ACSP.NET application</div>

	{Navbar}

	<div class="Content">
		{MainContent}
	</div>

	<div class="ExecutionTimeFooter">{LabelExecutionTime}: {SV:SiteExecutionTime}</div>
</body>
</html>
