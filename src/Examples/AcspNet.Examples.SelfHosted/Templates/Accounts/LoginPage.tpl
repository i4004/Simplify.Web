<form class="form-horizontal">
	<fieldset>
		<div class="form-group">
			<label for="Login" class="control-label col-xs-2">Login</label>
			<div class="col-xs-10">
				<input type="text" class="form-control" id="Login" placeholder="Login" value="{Login}">
			</div>
		</div>
		<div class="form-group">
			<label for="inputPassword" class="control-label col-xs-2">Password</label>
			<div class="col-xs-10">
				<input type="Password" class="form-control" id="Password" placeholder="Password">
			</div>
		</div>
		<div class="form-group">
			<div class="col-xs-offset-2 col-xs-10">
				<div class="checkbox">
					<label><input type="checkbox" name="RememberMe" {RememberMe}>Remember me</label>
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
