function togglePassword() {
  var passwordField = document.getElementById("password");
  var eyeClosed = document.getElementById("eyeClosed");
  var eyeOpen = document.getElementById("eyeOpen");

  if (passwordField.type === "password") {
      passwordField.type = "text";
      eyeClosed.style.display = "none";
      eyeOpen.style.display = "inline-block";
  } else {
      passwordField.type = "password";
      eyeClosed.style.display = "inline-block";
      eyeOpen.style.display = "none";
  }
}

document.getElementById('login-form').addEventListener('submit', async function(event) {
  event.preventDefault();

  const username = document.getElementById('username-login').value;
  const password = document.getElementById('password').value;

  const response = await fetch('https://localhost:7295/api/Login', {
      method: 'POST',
      headers: {
          'Content-Type': 'application/json'
      },
      body: JSON.stringify({ username, password })
  });

  if (response.ok) {
      alert('Login successful');
      window.location.href = `../Pages/LandingPage.html?username=${encodeURIComponent(username)}`;
  } else {
      alert('Login failed');
  }
});

window.onload = function () {
    const buttonBack = document.querySelector('.button-goBack');

    buttonBack.addEventListener('click', function () {
        window.location.href = '../Pages/LandingPage.html';
    });
}