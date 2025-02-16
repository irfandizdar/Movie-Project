document.addEventListener('DOMContentLoaded', () => {
    fetchMovies(); // Prikaži filmove po defaultu
});

function fetchMovies() {
    fetch('http://localhost:5283/api/Movies/GetAllMovies')
        .then(response => response.json())
        .then(data => {
            const contentArea = document.getElementById('content-area');
            contentArea.innerHTML = '';
            data.forEach(movie => {
                const movieCard = createMovieCard(movie);
                contentArea.appendChild(movieCard);
            });
        })
        .catch(error => console.error('Greška pri dohvaćanju filmova:', error));
}

function createMovieCard(movie) {
    const movieCard = document.createElement('div');
    movieCard.className = 'movie-card';

    const img = document.createElement('img');
    img.src = movie.posterUrl; 
    img.alt = movie.title;
    img.dataset.movieId = movie.id; 
    img.addEventListener('click', playVideo);

    const movieInfo = document.createElement('div');
    movieInfo.className = 'movie-info';
    movieInfo.innerHTML = `<h3>${movie.title}</h3><p>${new Date(movie.releaseDate).getFullYear()} • ${movie.durationMinutes} min • ${movie.genre}</p>`;

    movieCard.appendChild(img);
    movieCard.appendChild(movieInfo);

    return movieCard;
}

function playVideo(event) {
    const movieId = event.target.dataset.movieId;
    fetch(`http://localhost:5283/api/Movies/GetMovieVideoUrl/${movieId}`)
        .then(response => response.text())
        .then(videoUrl => {
            const iframe = document.createElement('iframe');
            iframe.width = '560';
            iframe.height = '315';
            iframe.src = videoUrl; 
            iframe.allowFullscreen = true;
            iframe.frameBorder = '0';

            const modal = document.createElement('div');
            modal.className = 'modal';
            modal.appendChild(iframe);

            document.body.appendChild(modal);
        })
        .catch(error => console.error('Greška pri dohvaćanju URL-a videa:', error));
}



function fetchSeries() {
    fetch('http://localhost:5283/api/Series/GetAllSeries')
        .then(response => response.json())
        .then(data => {
            const contentArea = document.getElementById('content-area');
            contentArea.innerHTML = '';
            data.forEach(series => {
                const seriesCard = createSeriesCard(series);
                contentArea.appendChild(seriesCard);
            });
        })
        .catch(error => console.error('Error fetching series:', error));
}

function createSeriesCard(series) {
    const seriesCard = document.createElement('div');
    seriesCard.className = 'movie-card';

    const img = document.createElement('img');
    img.src = series.posterUrl; 
    img.alt = series.title;
    img.addEventListener('click', () => playVideo(series.id, 'series'));

    const seriesInfo = document.createElement('div');
    seriesInfo.className = 'movie-info';
    seriesInfo.innerHTML = `<h3>${series.title}</h3><p>${new Date(series.firstAirDate).getFullYear()} • Seasons: ${series.numberOfSeasons} • ${series.genre}</p>`;

    seriesCard.appendChild(img);
    seriesCard.appendChild(seriesInfo);

    return seriesCard;
}

// Function to open modal with video
function openModal(videoUrl) {
    const modal = document.getElementById('modal');
    const videoPlayer = document.getElementById('video-player');
    videoPlayer.src = videoUrl;
    modal.style.display = 'block';
}

// Function to close modal
function closeModal() {
    const modal = document.getElementById('modal');
    const videoPlayer = document.getElementById('video-player');
    videoPlayer.src = '';
    modal.style.display = 'none';
}

// Function to show movies (example function)
function showMovies() {
    fetchMovies();
}

// Function to show series (example function)
function showSeries() {
    fetchSeries();
}

// Function to handle search (example function)
function search() {
    const query = document.getElementById('search-bar').value;
    Promise.all([
        fetch(`http://localhost:5283/api/Movies/GetMovieByTitle/${query}`).then(response => response.json()),
        fetch(`http://localhost:5283/api/Series/GetSeriesByTitle/${query}`).then(response => response.json())
    ])
    .then(data => {
        const dropdown = document.getElementById('movie-dropdown');
        dropdown.innerHTML = '';

        // Add movies to dropdown
        data[0].forEach(item => {
            const option = document.createElement('option');
            option.value = item.title; // Use the movie title as value
            option.textContent = `${item.title} (${new Date(item.releaseDate).getFullYear()}) - Movie`;
            dropdown.appendChild(option);
        });

        // Add series to dropdown
        data[1].forEach(item => {
            const option = document.createElement('option');
            option.value = item.title; // Use the series title as value
            option.textContent = `${item.title} (${new Date(item.firstAirDate).getFullYear()}) - Series`;
            dropdown.appendChild(option);
        });
    })
    .catch(error => console.error('Error fetching search results:', error));
}
