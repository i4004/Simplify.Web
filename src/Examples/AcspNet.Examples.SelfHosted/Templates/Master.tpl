<!DOCTYPE html>
<html>
<head>
	<meta charset="utf-8" />
	<title>{Title}</title>
	<link rel="stylesheet" type="text/css" href="{~}/Content/bootstrap.min.css" />
	<link rel="stylesheet" type="text/css" href="{~}/Styles/Main.css" />
</head>
<body>
	<script type="text/javascript" src="{~}/Scripts/jquery-2.1.1.min.js"></script>
	<script type="text/javascript" src="{~}/Scripts/bootstrap.min.js"></script>

	<div class="Header">
		<div class="Title"><img class="Logo" src="{~}/Images/Icon.png" alt="AcspNet"> Your ACSP.NET application</div>
		<div class="Menu">
			<div class="Left"><a href="{~}/">Home</a> <a href="{~}/about">About</a><a href="{~}/messagePageExample">Message page example</a></div>
			<div class="Right">{LoginPanel}</div>
		</div>
	</div>
	<div class="Content">
		<div class="MessageBox">{MainContent}</div>
	</div>

	<div class="ExecutionTimeFooter">{LabelExecutionTime}: {SV:SiteExecutionTime}</div>
</body>
</html>
