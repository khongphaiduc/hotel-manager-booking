function luxuryCardHover(card) {
    card.classList.add("lux-glow-active");
}
function luxuryCardOut(card) {
    card.classList.remove("lux-glow-active");
}
function luxBookNow(roomName) {
    document.getElementById("lux-modal-room").textContent = roomName;
    document.getElementById("lux-modal").classList.add("show");
}
function luxCloseModal() {
    document.getElementById("lux-modal").classList.remove("show");
}
document.addEventListener("keydown", function (e) {
    if (e.key === "Escape") luxCloseModal();
});

window.addEventListener('DOMContentLoaded', () => {
    const cards = document.querySelectorAll('.lux-room-card');
    cards.forEach((card, idx) => {
        setTimeout(() => {
            card.classList.add('animated');
        }, idx * 120); // hiệu ứng so le giữa các thẻ
    });
});

// ...giữ nguyên các hàm khác...
function luxuryCardHover(card) {
    card.classList.add("lux-glow-active");
}
function luxuryCardOut(card) {
    card.classList.remove("lux-glow-active");
}
function luxBookNow(roomName) {
    document.getElementById("lux-modal-room").textContent = roomName;
    document.getElementById("lux-modal").classList.add("show");
}
function luxCloseModal() {
    document.getElementById("lux-modal").classList.remove("show");
}
document.addEventListener("keydown", function (e) {
    if (e.key === "Escape") luxCloseModal();
});