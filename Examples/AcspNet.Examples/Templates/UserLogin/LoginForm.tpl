<script type="text/javascript">
    function Login()
    {
        //var resultPanel = $('#ResultPanel');
        //var membershipNumber = $('#MembershipNumber');
        //var password = $('#Password');

        //resultPanel.hide();

        //if (!ValidateFields([membershipNumber, password]))
        //{
        //	resultPanel.html(GetMessageBox("{MessageValidationError}"));
        //	resultPanel.show('slow');
        //	return;
        //}

        //if (!ValidateIntField(membershipNumber, 9))
        //{
        //	resultPanel.html(GetMessageBox("{MessageMembershipNumberValidationError}"));
        //	resultPanel.show('slow');
        //	return;
        //}
        //else
        //{
        //	var buttonOK = $('#ButtonOK');

        //	buttonOK.attr('disabled', 'disabled');
        //	$('#AjaxProgress').show();

        //	$.get("?act=login", { MembershipNumber: membershipNumber.val(), Password: password.val() },

        //	function (response)
        //	{
        //		if (response == "OK")
        //			$('#LoginForm').submit();
        //		else
        //		{
        //			$('#AjaxProgress').hide();
        //			resultPanel.html(response);
        //			resultPanel.show('slow');

        //			$('html, body').animate({
        //				scrollTop: resultPanel.offset().top
        //			}, 1000);a

        //			buttonOK.removeAttr("disabled");
        //		}
        //	});
        //}
    }
</script>
<!--<form action="?act=searchFlights" id="LoginForm" method="post">
<!--<div class="FormHeader HeaderBackground">{Header} </div>-->
<!--<div class="FormLabelValueContainer">
    <label class="FormLabel" for="MembershipNumber">{LabelUserName}:</label>
    <div class="FormValue">-->
<input name="UserName" type="text" id="UserName" onkeypress="ResetInvalidState(this); ClickByEnter(event, 'ButtonOK');" />
<!--</div>
</div>
<div class="FormLabelValueContainer">
    <label class="FormLabel" for="Password">{LabelPassword}:</label>
    <div class="FormValue">-->
<input name="Password" id="Password" type="password" onkeypress="ResetInvalidState(this); ClickByEnter(event, 'ButtonOK');" />
<!--</div>
</div>
<div id="ResultPanel"></div>
<div class="FormButton">-->
<input id="ButtonOK" name="ButtonOK" type="button" value="{LabelButtonOK}" onclick="Login()" />
<!--</div>
<div class="FormsAjaxProgressCell" id="AjaxProgress"></div>
<div class="FormFooter">{IntroMessage}</div>
</form>-->
-->