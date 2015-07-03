(function($){	
    $.confirm = function (params) {
		if($('#confirmOverlay').length){
			return false;
		}		
		var buttonHtml = '';
		$.each(params.buttons,function(name,obj){					
			buttonHtml += '<a href="#" class="uibutton large '+obj['class']+'">'+name+'<span></span></a>';			
			if(!obj.action){
				obj.action = function(){};
			}
		});
		
		  $('body').append('<div id="confirmOverlay"></div><div id="confirmBox"><h1>'+params.title+'</h1><p>'+params.message+'</p><div id="confirmButtons">'+buttonHtml+'</div></div>');
	    $('#confirmOverlay').css('opacity', 0.3).fadeIn(400, function() {
	        $('#confirmBox').fadeIn(200);
	    });
		var buttons = $('#confirmBox .uibutton');
		var i = 0;
		$.each(params.buttons,function(name,obj){
			buttons.eq(i++).click(function(){
				obj.action();
				$.confirm.hide();
				return false;
			});
		});
	}

	$.confirm.hide = function(){
		
		$('#confirmBox').fadeOut(function(){
				$(this).remove();						  
			$('#confirmOverlay').fadeOut(function() {
				$(this).delay(50).remove();
			  });	
		});
	}	
})(jQuery);