const elements = document.querySelectorAll('.element');
elements.forEach(el => {
    const id = el.dataset.id;
    let editBtn = el.querySelector('.edit');
    let title = el.querySelector('.title');

    editBtn.addEventListener('click', () => {
        editBtn.classList.add('disabled');
        title.classList.add('d-none');
        el.querySelector('.editable-group').classList.remove('d-none');
    });

    el.querySelector('.cancel').addEventListener('click', () => {
        location.reload();
    });
});