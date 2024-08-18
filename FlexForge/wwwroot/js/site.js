function toggleFavorite(button, productId) {
        var icon = button.querySelector('.fa-heart');
    icon.classList.toggle('favorite');
    if (icon.classList.contains('favorite')) {
        icon.style.color = 'lightred'; // Filled heart color
        } else {
        icon.style.color = 'gray'; // Empty heart color
        }

    // Submit the form programmatically
    document.getElementById('favoriteForm-' + productId).submit();
    }

