function togglePassword() {
  var passwordField = document.getElementById("password");
  var confirmPasswordField = document.getElementById("confirm-password");
  var eyeClosed = document.getElementById("eyeClosed");
  var eyeOpen = document.getElementById("eyeOpen");

  if (passwordField.type === "password") {
      passwordField.type = "text";
      confirmPasswordField.type = "text";
      eyeClosed.style.display = "none";
      eyeOpen.style.display = "inline-block";
  } else {
      passwordField.type = "password";
      confirmPasswordField.type = "password";
      eyeClosed.style.display = "inline-block";
      eyeOpen.style.display = "none";
  }
}

document.getElementById('register-form').addEventListener('submit', async function(event) {
  event.preventDefault();

  const username = document.getElementById('username').value;
  const password = document.getElementById('password').value;
  const confirmPassword = document.getElementById('confirm-password').value;
  const loadingElement = document.getElementById('loading');
  const buttonText = document.getElementById('button-text');
    
  if (password !== confirmPassword) {
      alert("Both passwords need to be the same.");
      return;
  }

  try {
    loadingElement.style.display = 'block';
    buttonText.classList.add('hide-text');

    const response = await fetch('https://localhost:7295/api/Register', {
      method: 'POST',
      headers: {
          'Content-Type': 'application/json'
      },
      body: JSON.stringify({ username, password })
  });

  if (response.ok) {
      alert('Register successful');
      window.location.href = '../Pages/LoginBox.html';
  } else if(errorResponse.code === 'USERNAME_TOO_LONG') {
    alert('Register failed! UserName Too Long.');
    loadingElement.style.display = 'none';
    buttonText.classList.remove('hide-text');
  } else if(errorResponse.code === 'PASSWORD_TOO_LONG') {
    alert('Register failed! Password Too Long.');
    loadingElement.style.display = 'none';
    buttonText.classList.remove('hide-text');
  } else if(errorResponse.code === 'USERS_LIMIT_REACHED') {
    alert('Register failed! Users limit reached.');
    loadingElement.style.display = 'none';
    buttonText.classList.remove('hide-text');
  }
   else {
      alert('Register failed! Name already used.');
      loadingElement.style.display = 'none';
      buttonText.classList.remove('hide-text');
  }

    } catch (error) {
        alert('Server offline. Not possible to connect.');

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