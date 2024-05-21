const videoInput = document.getElementById('videoInput');
const video = document.getElementById('video');

videoInput.addEventListener('input', function () {
	const videoUrl = this.value;

	// Проверка валидности ссылки (необязательно)
	if (!isValidURL(videoUrl)) {
		alert('Неверная ссылка на видео.');
		return;
	}

	video.src = videoUrl;

});

// Функция проверки валидности URL (необязательно)
function isValidURL(url) {
	const regex = /https?:\/\/(www\.)?[-a-zA-Z0-9@:%._\+~#=]{1,256}\.[a-zA-Z0-9()]{1,6}\b([-a-zA-Z0-9()@:%_\+.~#?&//=]*)/g;
	return regex.test(url);
}