/*
Menu Effect
*/

$(document).ready(function(){
	$('#nav1 a')
	.css( {backgroundPosition: "0 0"} )
	.mouseover(function(){
		$(this).stop().animate(
			{backgroundPosition:"(-280px 0px)"}, 
			{duration:1000})
		})
	.mouseout(function(){
		$(this).stop().animate(
			{backgroundPosition:"(0 0)"}, 
			{duration:1000})
		});
		
	$('#nav2 a')
	.css( {backgroundPosition: "0 0"} )
	.mouseover(function(){
		$(this).stop().animate(
			{backgroundPosition:"(300px 300px)"}, 
			{duration:1000})
		})
	.mouseout(function(){
		$(this).stop().animate(
			{backgroundPosition:"(0px 0)"}, 
			{duration:1000})
		});
		
	$('#nav3 a')
	.css( {backgroundPosition: "0 0"} )
	.mouseover(function(){
		$(this).stop().animate(
			{backgroundPosition:"(300px 250px)"}, 
			{duration:1000})
		})
	.mouseout(function(){
		$(this).stop().animate(
			{backgroundPosition:"(0 0)"}, 
			{duration:1000})
		});
});
