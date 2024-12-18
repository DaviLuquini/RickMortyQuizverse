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

document.getElementById("username").addEventListener("input", function () {
  this.value = this.value.replace(/\s/g, "");
});
document.getElementById("password").addEventListener("input", function () {
  this.value = this.value.replace(/\s/g, "");
});

document.getElementById('register-form').addEventListener('submit', async function(event) {
  event.preventDefault();

  const username = document.getElementById('username').value;
  const password = document.getElementById('password').value;
  const confirmPassword = document.getElementById('confirm-password').value;
  const loadingElement = document.getElementById('loading');
  const buttonText = document.getElementById('button-text');

  if (/\s/.test(username) || /\s/.test(password)) {
      alert("O nome de usuário e a senha não devem conter espaços em branco.");
      return;
  }
  if (password !== confirmPassword) {
      alert("Both passwords need to be the same.");
      return;
  }

  try {
    loadingElement.style.display = 'block';
    buttonText.classList.add('hide-text');

    const response = await fetch('https://p01--rickmortyquizverse--2fzvm2y2h546.code.run/api/register', {
      method: 'POST',
      headers: {
          'Content-Type': 'application/json'
      },
      body: JSON.stringify({ username, password })
  });

  const result = await response.json();

  if (response.ok) {
    alert('Register successful');
    window.location.href = '../Pages/LoginBox.html';
} else {

    if (result.code === 'USERNAME_TOO_LONG') {
        alert('Register failed! UserName Too Long.');
    } else if (result.code === 'PASSWORD_TOO_LONG') {
        alert('Register failed! Password Too Long.');
    } else if (result.code === 'USERS_LIMIT_REACHED') {
        alert('Register failed! Users limit reached.');
    } else if (result.code === 'NAME_ALREADY_USED') {
        alert('Register failed! Name Already Used.');
    } else {
        alert('Register failed! An unknown error occurred.'); 
    }
    
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
        window.location.href ="../index.html";
    });
}