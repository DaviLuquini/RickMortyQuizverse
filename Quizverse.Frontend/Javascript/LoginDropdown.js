function toggleDropdown() {
  var dropdown = document.getElementById('login-dropdown');
  if (dropdown.style.display === 'none' || dropdown.style.display === '') {
      dropdown.style.display = 'block';
  } else {
      dropdown.style.display = 'none';
  }
}

function login() {
  window.location.href = '/Pages/LoginBox.html';
}

function register() {
  window.location.href = '/Pages/RegisterBox.html';
}

window.onclick = function(event) {
  if (!event.target.matches('#login-icon')) {
      var dropdown = document.getElementById('login-dropdown');
      if (dropdown.style.display === 'block') {
          dropdown.style.display = 'none';
      }
  }
}