<form class="form-horizontal" method="post" id="Form">
	<fieldset>
		<div class="form-group">
			<label for="UserName" class="control-label col-xs-2">User name</label>
			<div class="col-xs-10">
				<input type="text" class="form-control" id="UserName" name="UserName" placeholder="User name" value="{Model.UserName}" />
			</div>
		</div>
		<div class="form-group">
			<label for="inputPassword" class="control-label col-xs-2">Password</label>
			<div class="col-xs-10">
				<input type="Password" class="form-control" id="Password" name="Password" placeholder="Password" />
			</div>
		</div>
		<div class="form-group">
			<div class="col-xs-offset-2 col-xs-10">
				<div class="checkbox">
					<label><input type="checkbox" name="RememberMe" {Model.RememberMe}/>Remember me</label>
				</div>
			</div>
		</div>
		<div class="form-group">
			<div class="col-xs-offset-2 col-xs-10">
				<button type="submit" class="btn btn-default">Login</button>
			</div>
		</div>
	</fieldset>
</form>
