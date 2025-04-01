function togglePasswordVisibility(passwordInputId, toggleButtonId) {
    var passwordInput = document.getElementById(passwordInputId);
    var toggleButton = document.getElementById(toggleButtonId);

    if (passwordInput.type === "password") {
        passwordInput.type = "text";
        toggleButton.innerHTML = '<i class="bi bi-eye-slash-fill"></i>';
    }
    else {
        passwordInput.type = "password";
        toggleButton.innerHTML = '<i class="bi bi-eye-fill"></i>';
    }
}