<div class="container" style="max-width: 400px">
	<form class="form-horizontal" method="post" id="Form">
		<div class="form-group">
			<input type="text" class="form-control" id="UserName" name="UserName" placeholder="User name" value="{Model.UserName}" />
		</div>
		<div class="form-group">
			<input type="Password" class="form-control" id="Password" name="Password" placeholder="Password" />
		</div>
		<div class="form-group">
			<div class="checkbox">
				<label><input type="checkbox" name="RememberMe" {model.rememberme} />Remember me</label>
			</div>
		</div>
		<div class="form-group">
			<button type="submit" class="btn btn-default">Login</button>
		</div>
	</form>
</div>
