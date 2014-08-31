<!DOCTYPE html>
<html>
<head>
	<meta charset="utf-8" />
	<title>{Title}</title>
	<link rel="stylesheet" type="text/css" href="{~}/Content/bootstrap.min.css" />
	<link rel="stylesheet" type="text/css" href="{~}/Content/bootstrap-theme.css" />
	<link rel="stylesheet" type="text/css" href="{~}/Styles/Main.css" />
</head>
<body>
	<script type="text/javascript" src="{~}/Scripts/jquery-2.1.1.min.js"></script>
	<script type="text/javascript" src="{~}/Scripts/bootstrap.min.js"></script>

	<div class="Title"><img class="Logo" src="{~}/Images/Icon.png" alt="AcspNet">Your ACSP.NET application</div>

	<nav class="navbar navbar-default" role="navigation">
		<div class="container-fluid">
			<div class="navbar-header">
				<button type="button" class="navbar-toggle collapsed" data-toggle="collapse" data-target="#bs-example-navbar-collapse-1">
					<span class="sr-only">Toggle navigation</span>
					<span class="icon-bar"></span>
					<span class="icon-bar"></span>
					<span class="icon-bar"></span>
				</button>
				<a class="navbar-brand" href="{~}/">Home</a>
			</div>

			<div class="collapse navbar-collapse" id="bs-example-navbar-collapse-1">
				<ul class="nav navbar-nav">
					<li><a href="{~}/about">About</a></li>
					<li><a href="{~}/messagePageExample">Message page example</a></li>
				</ul>
				<ul class="nav navbar-nav navbar-right">
					<li>{LoginPanel}</li>
				</ul>
			</div>
		</div>
	</nav>

	<div class="Content">
		{MainContent}
	</div>

	<div class="ExecutionTimeFooter">{LabelExecutionTime}: {SV:SiteExecutionTime}</div>
</body>
</html>
