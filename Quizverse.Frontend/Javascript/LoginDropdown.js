function toggleDropdown() {
  var dropdown = document.getElementById('login-dropdown');
  var profileImageBox = document.getElementById("profileImageBox");
  if (dropdown.style.display === 'none' || dropdown.style.display === '') {
      dropdown.style.display = 'block';
      profileImageBox.style.display = "none";
  } else {
      dropdown.style.display = 'none';
  }
}

function login() {
  window.location.href = '../Pages/LoginBox.html';
}

function register() {
  window.location.href = '../Pages/RegisterBox.html';
}

function changeImage() {
  var profileImageBox = document.getElementById("profileImageBox");
  var dropdown = document.getElementById('login-dropdown');
      profileImageBox.style.display = "block";
      dropdown.style.display = 'none';
}


function setProfileImage(imageSrc) {
  var profileImage = document.getElementById("login-icon");
  profileImage.src = imageSrc;

  profileImage.style.width = "92px";
  profileImage.style.height = "92px";
  profileImage.style.borderRadius = "50%";

  document.getElementById("profileImageBox").style.display = "none";
}



function hideElements(event) {
  if (!event.target.matches('#login-icon') && !event.target.closest('#login-dropdown') && !event.target.closest('#profileImageBox')) {
    
    var dropdown = document.getElementById('login-dropdown');
    if (dropdown.style.display === 'block') {
      dropdown.style.display = 'none';
    }

    var profileImage = document.getElementById('profileImageBox');
    if (profileImage.style.display === 'block') {
      profileImage.style.display = 'none';
    }
  }
}

window.onclick = function(event) {
  hideElements(event);
};

