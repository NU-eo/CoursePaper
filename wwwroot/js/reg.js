const EMAIL_REGEXP = /^(([^<>()[\].,;:\s@"]+(\.[^<>()[\].,;:\s@"]+)*)|(".+"))@(([^<>()[\].,;:\s@"]+\.)+[^<>()[\].,;:\s@"]{2,})$/iu;
const PASSWORD_REGEXP = /^(?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?=.*\W).{6,}$/;


const regForm = document.querySelector('#regForm');
const submitBtn = document.querySelector('#submitBtn');

submitBtn.addEventListener('click', (e) => {
    e.preventDefault()
    const password = document.getElementById('password');
    const passwordError = document.getElementById('passwordError');

    const email = document.getElementById('email');
    const emailError = document.getElementById('emailError');
    let errors = false
    if (isEmailValid(email.value)) {
        email.style.borderColor = 'green';
        emailError.textContent = ''
    } else {
        email.style.borderColor = 'red';
        emailError.textContent = 'Введите корректрую почту.'
        errors = true
    }

    if (isPasswordValid(password.value)) {
        password.style.borderColor = 'green';
        passwordError.textContent = ''
    } else {
        password.style.borderColor = 'red';
        passwordError.textContent = 'Пароль должен быть более 5 символов и содержать хотя бы одну заглавную английскую буква, строчная буква, цифру и спецсимвол.'
        errors = true
    }
    if (!errors) {
        regForm.submit();
    }
});


function isEmailValid(value) { return EMAIL_REGEXP.test(value); }
function isPasswordValid(value) { return PASSWORD_REGEXP.test(value); }