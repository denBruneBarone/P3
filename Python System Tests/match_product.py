from selenium import webdriver
from selenium.webdriver.common.by import By
import time
from selenium_utils import redirect, write_to_textbox, click_on_a_tag, generate_random_string, get_item_if_exists


driver = webdriver.Chrome('./chromedriver')

# Navigate
redirect(driver, "https://localhost:7027/")
click_on_a_tag(driver, "loginasadmin")
redirect(driver, "https://localhost:7027/match", 3)

# Get all unknown products
unknown_products_count = len(driver.find_elements(by=By.XPATH, value="//ul[@id='productListItems']/li/button"))

# For loop for doing the test for all unknow products
for i in range(unknown_products_count):
    time.sleep(.2) # Wait to encure all somponents has been mounted

    # Click the 
    driver.execute_script("arguments[0].click();", driver.find_element(by=By.NAME, value="apply-btn"))
    time.sleep(.2)


    # After apply
    
    left_id_success, left_id_elem = get_item_if_exists(driver, "left-id")
    id_values_after = (driver.find_element(by=By.NAME, value="ProductId_value").text, left_id_elem.text if left_id_success else None)

    left_titlegws_success, left_titlegws_elem = get_item_if_exists(driver, "left-titlegws")
    titlegws_values_after = (driver.find_element(by=By.NAME, value="TitleGWS_value").text, left_titlegws_elem.text if left_titlegws_success else None)

    left_minorder_success, left_minorder_elem = get_item_if_exists(driver, "left-minOrder")
    minorder_values_after = (driver.find_element(by=By.NAME, value="MinOrder_value").text, left_minorder_elem.text if left_minorder_success else None)

    left_target_success, left_target_elem = get_item_if_exists(driver, "left-target")
    target_values_after = (driver.find_element(by=By.NAME, value="Target_value").text, left_target_elem.text if left_target_success else None)

    if len(set(id_values_after)) == 1 and len(set(titlegws_values_after)) == 1 and len(set(minorder_values_after)) == 1 and len(set(target_values_after)) == 1:
        print(f"Success for product {i+1}")    
    else:
        print(f"Success for product {i+1}")    

    # accept
    driver.execute_script("arguments[0].click();", driver.find_element(by=By.NAME, value="accept-btn"))
    time.sleep(.5)
    
# Close
input("Press enter to exit")
driver.close()