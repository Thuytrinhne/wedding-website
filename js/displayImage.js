var loadImageButton = document.getElementById("loadImageButton");
loadImageButton.addEventListener("click", function (event) {
	event.preventDefault(); // Ngăn chặn hành vi mặc định của thẻ <a>
	console.log("đã vào chặn thẻ a");
});
