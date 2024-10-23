import { updateUserImage } from "./UserImage.js";

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

export function setProfileImage(intValue, isLoginCall = false) {
  var imageSrc = getSrcFromInt(intValue);

  if (isLoginCall) {
    updateProfileImageElement(imageSrc); 
    return;
  }
  updateProfileImageElement(imageSrc); 

  updateUserImage(intValue); 
}

document.addEventListener('DOMContentLoaded', () => {
  const loginIcon = document.getElementById('login-icon');
  loginIcon.addEventListener('click', toggleDropdown);

  const loginButton = document.getElementById('login-button');
  loginButton.addEventListener('click', login);

  const registerButton = document.getElementById('register-button');
  registerButton.addEventListener('click', register);

  const changeImageButton = document.getElementById('changeImage-button');
  changeImageButton.addEventListener('click', changeImage);

  const selectableImages = document.querySelectorAll('.selectableImage');
  selectableImages.forEach(image => {
      image.addEventListener('click', () => {
          const intValue = image.getAttribute('data-value');
          setProfileImage(intValue);
      });
  });
});

function updateProfileImageElement(imageSrc) {
  var profileImage = document.getElementById("login-icon");
  profileImage.src = imageSrc;

  profileImage.style.width = "92px";
  profileImage.style.height = "92px";
  profileImage.style.borderRadius = "50%";

  document.getElementById("profileImageBox").style.display = "none";
}


function getSrcFromInt(intValue) {
    switch (parseInt(intValue, 10)) {
        case 1:
            return "../css/Assets/Characters/rickImage.jpeg";
        case 2:
            return "../css/Assets/Characters/mortyImage.jpeg";
        case 3:
            return "../css/Assets/Characters/summerImage.jpeg";
        case 4:
            return "../css/Assets/Characters/bethImage.jpeg";
        case 5: 
            return "../css/Assets/Characters/jerryImage.jpeg";
        default:
            return "../css/Assets/iconLogin.png"; 
    }
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

