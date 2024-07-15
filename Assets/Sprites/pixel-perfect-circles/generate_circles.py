import pygame
import sys
import os

# Initialize Pygame
pygame.init()

# Set up the display
screen_width = 400
screen_height = 400
screen = pygame.display.set_mode((screen_width, screen_height))
pygame.display.set_caption("Pixel Perfect Filled Circles")

# Define colors
WHITE = (255, 255, 255)
BLACK = (0, 0, 0)

# Directory to save the PNG files
output_directory = "output_circles"
os.makedirs(output_directory, exist_ok=True)

# Main loop
def draw_circle(radius, filename):
    surface = pygame.Surface((radius * 2, radius * 2), pygame.SRCALPHA)
    pygame.draw.circle(surface, WHITE, (radius, radius), radius)
    pygame.image.save(surface, filename)

# Draw circles and save as .png
for r in range(200, 240):
    filename = os.path.join(output_directory, f"circle_radius_{r}.png")
    draw_circle(r, filename)

# Quit Pygame
pygame.quit()
sys.exit()