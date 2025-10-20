// intro.js (no per-char splitting). Curtains open slowly on load (adjusted durations).
// - On load: open curtains after a short delay
// - On continue: close curtains back to center, then navigate
// - Animations for whole title/greeting

document.addEventListener('DOMContentLoaded', function () {
    var curtains = document.getElementById('curtains');
    var left = curtains.querySelector('.curtain.left');
    var right = curtains.querySelector('.curtain.right');
    var content = document.querySelector('.intro-content');
    var continueBtn = document.getElementById('continueBtn');

    var mainTitleEl = document.querySelector('.main-title');
    var greetingEl = document.querySelector('.greeting');

    // read curtain durations from CSS variables (in seconds)
    var computed = getComputedStyle(document.documentElement);
    var leftDuration = parseFloat(computed.getPropertyValue('--curtain-duration-left')) || 3.6;
    var rightDuration = parseFloat(computed.getPropertyValue('--curtain-duration-right')) || 4.0;
    var maxDuration = Math.max(leftDuration, rightDuration);

    // initial delay before starting to open (ms) - increased for slower, cinematic feel
    var startDelay = 900; // ms

    // fallback timeout: wait a bit longer than the longest curtain duration to reveal content
    var fallbackTimeout = Math.round(maxDuration * 1000) + 1200; // ms

    // Ensure button disabled initially
    continueBtn.classList.add('disabled');
    continueBtn.setAttribute('aria-disabled', 'true');

    // Start opening after startDelay to give "entrance" feeling
    setTimeout(function () {
        curtains.classList.add('opened');
    }, startDelay);

    // Wait for both curtain transitions to end, then reveal content
    var completed = 0;
    function onCurtainTransition(e) {
        if (e.propertyName && !/transform|opacity/.test(e.propertyName)) return;
        completed++;
        if (completed >= 2) {
            // reveal content
            content.classList.add('revealed');

            // trigger whole-element animations
            if (mainTitleEl) mainTitleEl.classList.add('animate');
            if (greetingEl) greetingEl.classList.add('animate');

            // enable continue button
            continueBtn.classList.remove('disabled');
            continueBtn.removeAttribute('aria-disabled');

            try { continueBtn.focus({ preventScroll: true }); } catch (err) { continueBtn.focus(); }

            // add subtle floating after animation finishes
            var approxTitleDuration = 900; // ms (matches CSS)
            setTimeout(function () {
                if (mainTitleEl) mainTitleEl.classList.add('floating');
            }, approxTitleDuration + 200);

            left.removeEventListener('transitionend', onCurtainTransition);
            right.removeEventListener('transitionend', onCurtainTransition);
        }
    }
    left.addEventListener('transitionend', onCurtainTransition);
    right.addEventListener('transitionend', onCurtainTransition);

    // ESC to skip
    document.addEventListener('keydown', function (e) {
        if (e.key === 'Escape' || e.key === 'Esc') {
            if (!content.classList.contains('revealed')) {
                curtains.classList.add('opened');
                setTimeout(function () {
                    content.classList.add('revealed');
                    if (mainTitleEl) mainTitleEl.classList.add('animate');
                    if (greetingEl) greetingEl.classList.add('animate');
                    continueBtn.classList.remove('disabled');
                    continueBtn.removeAttribute('aria-disabled');
                    try { continueBtn.focus({ preventScroll: true }); } catch (err) { continueBtn.focus(); }
                }, 700);
            }
        }
    });

    // Continue: fade content, then close curtains (remove 'opened') and navigate after curtains closed
    continueBtn.addEventListener('click', function (ev) {
        if (continueBtn.classList.contains('disabled') || continueBtn.getAttribute('aria-disabled') === 'true') {
            ev.preventDefault(); return;
        }
        ev.preventDefault();
        var href = continueBtn.getAttribute('href');

        // fade out content
        content.style.transition = 'opacity 0.45s ease, transform 0.45s cubic-bezier(.2,.9,.2,1)';
        content.style.opacity = '0';
        content.style.transform = 'translateY(-10px) scale(0.996)';

        // start closing curtains shortly after
        setTimeout(function () {
            curtains.classList.remove('opened');
        }, 80);

        // wait full curtain-close duration (use the longer one) then navigate
        var closeWait = maxDuration;
        setTimeout(function () {
            window.location.href = href;
        }, Math.round(closeWait * 1000) + 260);
    });

    // Safety fallback: if transitions don't fire, reveal after fallbackTimeout
    setTimeout(function () {
        if (!content.classList.contains('revealed')) {
            content.classList.add('revealed');
            if (mainTitleEl) mainTitleEl.classList.add('animate');
            if (greetingEl) greetingEl.classList.add('animate');
            continueBtn.classList.remove('disabled');
            continueBtn.removeAttribute('aria-disabled');
        }
    }, fallbackTimeout);
});