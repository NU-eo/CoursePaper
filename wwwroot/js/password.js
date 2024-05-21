const PASSWORD_REGEXP = /^(?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?=.*\W).{6,}$/;


const changeForm = document.querySelector('#changeForm');
const submitBtn = document.querySelector('#submitBtn');

submitBtn.addEventListener('click', (e) => {
    e.preventDefault()
    let errors = false

    const password = document.getElementById('password');
    const passwordError = document.getElementById('passwordError');

    if (isPasswordValid(password.value)) {
        password.style.borderColor = 'green';
        passwordError.textContent = ''
    } else {
        password.style.borderColor = 'red';
        passwordError.textContent = 'Пароль должен быть более 5 символов и содержать хотя бы одну заглавную английскую буква, строчная буква, цифру и спецсимвол.'
        errors = true
    }
    if (!errors) {
        changeForm.submit();
    }
});

function isPasswordValid(value) { return PASSWORD_REGEXP.test(value); }