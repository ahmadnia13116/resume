let scrollHandler = null;
let dotNetRef = null;

window.addScrollListener = function (ref) {

    dotNetRef = ref;

    if (scrollHandler) {
        window.removeEventListener('scroll', scrollHandler);
    }

    scrollHandler = function () {
        const scrollY = window.scrollY || document.documentElement.scrollTop || 0;

        if (dotNetRef) {
            dotNetRef.invokeMethodAsync('UpdateNavOnScroll', Math.floor(scrollY));
        }
    };

    window.addEventListener('scroll', scrollHandler, { passive: true });

    scrollHandler();
};

window.removeScrollListener = function () {
    if (scrollHandler) {
        window.removeEventListener('scroll', scrollHandler);
    }

    scrollHandler = null;
    dotNetRef = null;
};