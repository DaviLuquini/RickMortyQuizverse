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
    const loadingElement = document.getElementById('loading');
    const buttonText = document.getElementById('button-text');
  
    try {
      loadingElement.style.display = 'block';
      buttonText.classList.add('hide-text');

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
      
    } catch (error) {
      alert('Servidor offline. Não foi possível conectar.');

      loadingElement.style.display = 'none';
      buttonText.classList.remove('hide-text');
    }
  });

window.onload = function () {
    const buttonBack = document.querySelector('.button-goBack');

    buttonBack.addEventListener('click', function () {
        window.location.href = '../Pages/LandingPage.html';
    });
}