Feature: Iframe

Scenario: FrameSwitch_w3schools_StaleElement
	* 'https://www.w3schools.com/html/html_iframe.asp' URL açılır
	* W3Schools iframe geçişi yapılır
	* İlk bulunan div elementinin tagname'i okunur
	* 'https://www.w3schools.com/html/html_iframe.asp' URL açılır
	* İlk bulunan div elementinin tagname'i okunur

Scenario: FrameSwitch_seleniumbase_demo
	* 'https://seleniumbase.io/demo_page' URL açılır
	* DemoPage iframe geçişi yapılır
	* Checkbox işaretlenir
	* Checkbox'ın işaretli olması kontrol edilir

Scenario: FrameSwitch_frametester
	* 'https://iframetester.com/?url=https://en.wikipedia.org' URL açılır
	* iframetester iframe geçişi yapılır
	* Wikipedia başlığı tıklanır
	* Wikipedia logosu üzerindeki yazı kontrol edilir