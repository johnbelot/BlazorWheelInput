function setupScrollSnapDetection(container, dotNetReference, offsetItemCount) {

    let debounceTimer;

    container.addEventListener('scroll', () => {
        if (debounceTimer) {
            clearTimeout(debounceTimer);
        }

        debounceTimer = setTimeout(() => {
            let closestElement = null;
            let closestElementIndex = -1;
            let minDistance = Infinity;
            let innerText = "";
            let totalOffsetHeight = 0;

            // Calculate the total height of offset items
            const items = container.querySelectorAll('.scroll-item');
            for (let i = 0; i < offsetItemCount && i < items.length; i++) {
                totalOffsetHeight += items[i].offsetHeight;
            }

            const containerTop = container.getBoundingClientRect().top + totalOffsetHeight;
            container.querySelectorAll('.scroll-item').forEach((item, index) => {
                const itemTop = item.getBoundingClientRect().top;
                const distance = Math.abs(itemTop - containerTop);
                console.log(distance);
                if (distance < minDistance) {
                    minDistance = distance;
                    closestElement = { index, id: item.id };
                    innerText = item.innerText.trim();
                    closestElementIndex = index;
                }
            });

            if (closestElement) {
                dotNetReference.invokeMethodAsync('SetSnappedElement', closestElementIndex);
            }
        }, 2);
    });
}

function scrollToItem(container, index, offsetItemCount = 0) {
    const items = container.querySelectorAll('.scroll-item');
    if (index < 0 || index >= items.length) {
        console.error("Invalid index");
        return;
    }

    let totalOffsetHeight = 0;
    for (let i = 0; i < offsetItemCount && i < items.length; i++) {
        totalOffsetHeight += items[i].offsetHeight;
    }

    const itemToScrollTo = items[index];
    const scrollPosition = itemToScrollTo.offsetTop - totalOffsetHeight;
    container.scrollTo({ top: scrollPosition, behavior: 'instant' });
}