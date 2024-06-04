document.addEventListener("DOMContentLoaded", () => {
	const apiUrl = "https://wedding3-production.up.railway.app/posts?limit=1000";
	getData(apiUrl);

	// Search
	// Lấy thẻ input và form từ DOM
	const searchInput = document.getElementById("searchInput");

	if (searchInput) {
		searchInput.addEventListener("input", function () {
			search(apiUrl, searchInput.value);
		});
	}
});

function clearInputPost() {
	document.getElementById("name").value = "";
	document.getElementById("message").value = "";
	document.getElementById("contact").value = "";
}

function checkValidDataPost(name, content, contact) {
	const nameError = document.getElementById("error-name");
	const contactError = document.getElementById("error-contact");
	const contentError = document.getElementById("error-content");
	nameError.innerHTML = "";
	contactError.innerHTML = "";
	contentError.innerHTML = "";

	if (name == "") {
		nameError.innerHTML = "Vui lòng nhập tên của bạn !";
		return false;
	} else if (contact == "") {
		contactError.innerHTML = "Vui lòng nhập thông tin liên hệ của bạn !";
		return false;
	} else if (content == "") {
		contentError.innerHTML = "Vui lòng nhập lời chúc của bạn !";
		return false;
	} else {
		return true;
	}
}
// Hàm thực hiện tìm kiếm

function search(apiUrl, keyword) {
	console.log("đã vào search function");
	// Thêm query parameter vào URL
	const urlWithParams = new URL(apiUrl);
	const trimmedKeyword = keyword.trim();

	if (trimmedKeyword != "")
		urlWithParams.searchParams.append("keyword", trimmedKeyword);

	fetch(urlWithParams)
		.then((response) => {
			if (!response.ok) {
				throw new Error("Network response was not ok");
			}
			return response.json();
		})
		.then((data) => {
			console.log(data);
			showPostIntoPage(data);

			if (data.totalRecords == null) showTotalPost(0);
			else showTotalPost(data.totalRecords);
		})
		.catch((error) => {
			console.error("Error:", error);
		});
}

function getData(apiUrl) {
	// Function to handle GET request
	console.log("Thực hiện log data");
	fetch(apiUrl)
		.then((response) => {
			if (!response.ok) {
				throw new Error("Network response was not ok");
			}
			return response.json();
		})
		.then((data) => {
			showPostIntoPage(data);
			if (data.totalRecords == null) showTotalPost(0);
			else showTotalPost(data.totalRecords);
		})
		.catch((error) => {
			console.error("Error:", error);
		});
}

function showTotalPost(totalItems) {
	const totalPost = document.getElementById("totalMessage");
	totalPost.innerHTML = "";
	totalPost.innerHTML = totalItems;
}
function showPostIntoPage(data) {
	const listContainer = document.getElementById("list-container");
	if (listContainer) {
		// Xóa các phần tử hiện tại trong container
		listContainer.innerHTML = "";
		// Access the data array and loop through each item
		data.data.forEach((item, index) => {
			// Create elements for each item
			if (index !== 0) {
				const hr = document.createElement("hr");
				hr.className = "inner-li";

				listContainer.appendChild(hr);
			}
			const listItem = document.createElement("li");
			listItem.className = "list-group-item list-group-item-action inner-li";

			const firstDiv = document.createElement("div");
			firstDiv.style.display = "flex";

			const personSpan = document.createElement("span");
			personSpan.className = "material-symbols-outlined";
			personSpan.style.marginTop = "4px";
			personSpan.textContent = "person";

			const nameHeading = document.createElement("h6");
			nameHeading.className = "li-post li-post-name";
			nameHeading.style.color = "#0f0f0f";
			nameHeading.textContent = item.name;

			const timeLink = document.createElement("a");
			timeLink.style.color = "#606060";
			timeLink.style.fontSize = "80%";
			timeLink.style.marginTop = "2px";
			timeLink.style.marginLeft = "10px";
			timeLink.style.cursor = "pointer";

			// Chuyển đổi chuỗi thời gian thành định dạng mong muốn
			const createdAt = new Date(item.createdAt);
			const options = {
				weekday: "long",
				day: "numeric",
				month: "long",
				year: "numeric",
				hour: "numeric",
				minute: "numeric",
			};
			const formattedTime = createdAt.toLocaleString("vi-VN", options);

			timeLink.title = formattedTime;

			timeLink.textContent = item.timeSincePost;

			const secondDiv = document.createElement("div");
			secondDiv.style.display = "flex";

			const commentSpan = document.createElement("span");
			commentSpan.className = "material-symbols-outlined";
			commentSpan.style.marginRight = "4px";
			commentSpan.textContent = "comment";

			const contentParagraph = document.createElement("p");
			contentParagraph.className = "li-post";

			contentParagraph.style.color = "#0f0f0f";
			contentParagraph.textContent = item.content;

			// Append elements to their respective parent elements
			firstDiv.appendChild(personSpan);
			firstDiv.appendChild(nameHeading);
			firstDiv.appendChild(timeLink);
			listItem.appendChild(firstDiv);

			secondDiv.appendChild(commentSpan);
			secondDiv.appendChild(contentParagraph);
			listItem.appendChild(secondDiv);

			// Append the list item to the list container
			listContainer.appendChild(listItem);
		});
	}
}
function postPost(apiUrl, postData) {
	console.log("Dda vao post");
	fetch(apiUrl, {
		method: "POST",
		headers: {
			"Content-Type": "application/json",
		},
		body: JSON.stringify(postData),
	})
		.then((response) => {
			if (!response.ok) {
				throw new Error("Network response was not ok");
			}
		})
		.then((data) => {
			console.log("Success:", data);
			swal(
				"Gửi lời chúc thành công",
				`Cảm ơn ${postData["name"]} rất nhiều!`,
				"success"
			);
			clearInputPost();
			getData(apiUrl);
		})
		.catch((error) => {
			console.error("Error:", error);
		});
}
function addMessage() {
	const name = document.getElementById("name").value.trim();
	const content = document.getElementById("message").value.trim();
	const contact = document.getElementById("contact").value.trim();

	if (checkValidDataPost(name, content, contact)) {
		// Prepare the data to be sent in the POST request
		const postData = {
			name: name,
			contact: contact,
			content: content,
		};
		console.log(postData);

		const apiUrl = "https://wedding3-production.up.railway.app/posts";
		postPost(apiUrl, postData);
	}
}

// coundown
// Định nghĩa ngày kết thúc
var endDate = new Date("2024/07/07 11:00:00");

function updateCountdown() {
	// Lấy thời gian hiện tại
	var currentDate = new Date();
	if (endDate > currentDate) {
		// Tính toán số ngày còn lại
		var timeRemaining = endDate.getTime() - currentDate.getTime();
		var days = Math.floor(timeRemaining / (1000 * 60 * 60 * 24));

		// Tính toán số giờ, phút, giây còn lại
		var hours = Math.floor(
			(timeRemaining % (1000 * 60 * 60 * 24)) / (1000 * 60 * 60)
		);
		var minutes = Math.floor((timeRemaining % (1000 * 60 * 60)) / (1000 * 60));
		var seconds = Math.floor((timeRemaining % (1000 * 60)) / 1000);

		// Cập nhật giá trị vào các thẻ <span>
		document.getElementById("days").textContent = days;
		document.getElementById("hours").textContent = hours;
		document.getElementById("minutes").textContent = minutes;
		document.getElementById("seconds").textContent = seconds;
	}
}

// Cập nhật lần đầu
updateCountdown();

// Cập nhật giá trị liên tục sau mỗi giây
setInterval(updateCountdown, 1000);

// play music
var audio = document.getElementById("myAudio");
var playButton = document.getElementById("playButton");

playButton.addEventListener("click", function () {
	if (audio.paused) {
		audio.play();
		playButton.innerHTML =
			'<span class="material-symbols-outlined circle-shape"  title="Click vào đây để tắt nhạc">music_off</span>';
	} else {
		audio.pause();
		playButton.innerHTML =
			'<span class="material-symbols-outlined circle-shape"  title="Click vào đây để nghe nhạc">music_note</span>';
	}
});
