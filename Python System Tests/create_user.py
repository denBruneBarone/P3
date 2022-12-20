from selenium import webdriver
from selenium.webdriver.common.by import By
import time
from selenium_utils import redirect, write_to_textbox, click_on_a_tag, generate_random_string

driver = webdriver.Chrome('./chromedriver')

# Navigate
redirect(driver, "https://localhost:7027/")
click_on_a_tag(driver, "loginasadmin")
click_on_a_tag(driver, "usermgmtbtn", 3)

# Get prev user count
previous_user_count = len(driver.find_elements(by=By.XPATH, value="//table[@name='user-table']/tbody/tr"))

# Fill in user info
write_to_textbox(driver, "username", generate_random_string("test"))
write_to_textbox(driver, "password", generate_random_string())
write_to_textbox(driver, "fullname", generate_random_string("test_user"))

# Create user 
time.sleep(1)
driver.find_element(by=By.NAME, value="add-user").click()
time.sleep(1)

# Get new user count
new_user_count = len(driver.find_elements(by=By.XPATH, value="//table[@name='user-table']/tbody/tr"))

# Conclude test
print(f"Old user count: {previous_user_count}\nNew user count: {new_user_count}")
print(f"Test {'completed' if new_user_count-previous_user_count == 1 else 'failed'}!")

# Close
input("Press enter to exit")
driver.close()