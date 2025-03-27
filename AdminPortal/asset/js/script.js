//script.js 
const cardsPerPage = 12; // Number of cards to show per page 
const dataContainer = document.getElementById('data-container');
const pagination = document.getElementById('pagination');
const prevButton = document.getElementById('prev');
const nextButton = document.getElementById('next');
const pageNumbers = document.getElementById('page-numbers');
const pageLinks = document.querySelectorAll('.page-link');

const cards = Array.from(dataContainer.getElementsByClassName('card'));

// Calculate the total number of pages 
const totalPages = Math.ceil(cards.length / cardsPerPage);
let currentPage = 1;

// Function to display cards for a specific page 
function displayPage(page) {
    const startIndex = (page - 1) * cardsPerPage;
    const endIndex = startIndex + cardsPerPage;
    cards.forEach((card, index) => {
        if (index >= startIndex && index < endIndex) {
            card.style.display = 'block';
        } else {
            card.style.display = 'none';
        }
    });
}

// Function to update pagination buttons and page numbers 
function updatePagination() {
    pageNumbers.textContent =
        `Page ${currentPage} of ${totalPages}`;
    prevButton.disabled = currentPage === 1;
    nextButton.disabled = currentPage === totalPages;
    pageLinks.forEach((link) => {
        const page = parseInt(link.getAttribute('data-page'));
        link.classList.toggle('active', page === currentPage);
    });
}

// Event listener for "Previous" button 
prevButton.addEventListener('click', () => {
    if (currentPage > 1) {
        currentPage--;
        displayPage(currentPage);
        updatePagination();
    }
});

// Event listener for "Next" button 
nextButton.addEventListener('click', () => {
    if (currentPage < totalPages) {
        currentPage++;
        displayPage(currentPage);
        updatePagination();
    }
});

// Event listener for page number buttons 
pageLinks.forEach((link) => {
    link.addEventListener('click', (e) => {
        e.preventDefault();
        const page = parseInt(link.getAttribute('data-page'));
        if (page !== currentPage) {
            currentPage = page;
            displayPage(currentPage);
            updatePagination();
        }
    });
});

// Initial page load 
displayPage(currentPage);
updatePagination();


//Active buttons
var btnContainer = document.getElementById('btnProductDetailsIndicatorContainer');

var productDetailsBtn = btnContainer.getElementsByClassName('btnProductDetailsIndicator');


productDetailsBtn[i].addEventListener('click', activeFunction);

function activeFunction() {
    for (var i = 0; i < productDetailsBtn.length; i++) {
        var current = document.getElementsByClassName('btnProductDetailsActive');

        // If there's no btnProductDetailsActive class
        if (current.length > 0) {
            current[0].className = current[0].className.replace(' btnProductDetailsActive', '');
        }

        // Add the active class to the current/clicked button
        this.className += ' btnProductDetailsActive';
    }
};

